using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using RepoLayer.IRepos;
using SendGrid.Helpers.Errors.Model;
using ServiceLayer.IServices;
using ServiceLayer.Utils;

namespace ServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        private readonly IConfirmEmailService _emailService;
        private readonly IJwtUtil _jwtUtil;

        public UserService(IUserRepo repo, IConfirmEmailService emailService,
            IJwtUtil jwtUtil)
        {
            _repo = repo;
            _emailService = emailService;
            _jwtUtil = jwtUtil;
        }
        
        public IEnumerable<User> GetAllUser()
        {
            return _repo.GetAllUser();
        }

        public User? GetUser(int id)
        {
            return _repo.GetUser(id);
        }
        public User FindUser(int id)
        {
            return _repo.FindUser(id);
        }

        public User? GetUser(string email)
        {
            return _repo.GetUser(email);
        }

        public AuthenticateUser Login(string email, string password)
        {
            var user = _repo.GetUser(email);
            if (user == null) throw new BadRequestException("Email does not exist.");
            if (!user.EmailConfirmed) throw new UnauthorizedException($"{user.Id}");
            if (!user.IsActive) throw new ForbiddenException("Your account is banned by keepdaily due to against the rule.");

            var res = new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, password);
            if (res == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid password.");
            }
            else if (res == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.Password = new PasswordHasher<User>().HashPassword(user, password);
                _repo.SaveChanges();
            }

            return RefreshToken(user);
        }

        public AuthenticateUser RefreshToken(User user)
        {
            var jwtToken = _jwtUtil.GenerateJwtToken(user);
            var refreshToken = _jwtUtil.GenerateRefreshToken(user);
            return new AuthenticateUser(user, jwtToken, refreshToken);
        }

        public async Task<User> RegisterAsync(string name, string email, string password)
        {
            if (_repo.GetUser(email) != null)
                throw new BadRequestException("Email had already existed.");

            // Encrypt password
            User user = new () { Name = name, Email = email, IsActive = true };
            user.Password = new PasswordHasher<User>().HashPassword(user, password);

            // Save to database
            _repo.InsertUser(user);

            // Send email comfirmation
            await _emailService.SendConfirmEmailAsync(user);

            return user;
        }

        public User UpdateUserInfo(User user)
        {
            var dbUser = _repo.FindUser(user.Id);
            dbUser.Name = user.Name;
            if(user.Password != null)
                dbUser.Password = new PasswordHasher<User>().HashPassword(dbUser, user.Password);
            dbUser.Description = user.Description;
            dbUser.EmailNotify = user.EmailNotify;
            dbUser.LineNotify = user.LineNotify;
            _repo.SaveChanges();
            return dbUser;
        }

        public void UpdatePassword(int id, string password)
        {
            var user = _repo.FindUser(id);
            user.Password = new PasswordHasher<User>().HashPassword(user, password);
            _repo.SaveChanges();
        }

        public void UpdatePhoto(int id, string imgName, string imgType)
        {
            var user = _repo.FindUser(id);
            user.ImgName = imgName;
            user.ImgType = imgType;
            _repo.SaveChanges();
        }

        public void UpdateUserLineToken(string email, string token)
        {
            var user = _repo.GetUser(email) ?? throw new KeyNotFoundException($"User(Email:{email}) does not exist.");
            user.LineAccessToken = token;
            user.LineNotify = true;
            _repo.SaveChanges();
        }

        public void SaveChanges()
        {
            _repo.SaveChanges();
        }

        public void InActiveUser(int id)
        {
            throw new NotImplementedException();
        }

        /*
         * Google
         */
        public AuthenticateUser LoginWithGoogle(User user)
        {
            if (!user.EmailConfirmed) throw new UnauthorizedException($"{user.Id}");
            if (!user.IsActive) throw new ForbiddenException("Your account is banned by keepdaily due to against the rule.");
            return RefreshToken(user);
        }
        public AuthenticateUser RegisterWithGoogle(User user)
        {
            _repo.InsertUser(user);
            return RefreshToken(user);
        }
    }
}
