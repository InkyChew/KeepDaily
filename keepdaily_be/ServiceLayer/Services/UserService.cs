using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using RepoLayer.IRepos;
using SendGrid.Helpers.Errors.Model;
using ServiceLayer.IServices;

namespace ServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        private readonly IConfirmEmailService _emailService;

        public UserService(IUserRepo repo, IConfirmEmailService emailService)
        {
            _repo = repo;
            _emailService = emailService;
        }

        public User? GetUser(int id)
        {
            return _repo.GetUser(id);
        }
        public User FindUser(int id)
        {
            return _repo.FindUser(id);
        }

        public User Login(string email, string password)
        {
            var user = _repo.GetUser(email);
            if (user == null) throw new BadRequestException("Email does not exist.");
            if (!user.EmailConfirmed) throw new ForbiddenException($"{user.Id}");

            var res = new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, password);
            if(res == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid password.");
            }
            else if (res == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.Password = new PasswordHasher<User>().HashPassword(user, password);
                _repo.SaveChanges();
            }

            return user;
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

        public User UpdateUser(User user)
        {
            var dbUser = _repo.FindUser(user.Id);
            dbUser.Name = user.Name;
            dbUser.Password = user.Password;
            _repo.SaveChanges();
            return dbUser;
        }

        public void UpdateUserLineToken(string email, string token)
        {
            var user = _repo.GetUser(email) ?? throw new KeyNotFoundException($"User(Email:{email}) does not exist.");
            user.LineAccessToken = token;
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
    }
}
