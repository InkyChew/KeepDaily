using DomainLayer.Models;
namespace DomainLayer.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Description { get; set; }
        public string? ImgName { get; set; }
        public string? ImgType { get; set; }
        public bool EmailConfirmed { get; set; } = false;
        public string? LineId { get; set; }
        public string? LineAccessToken { get; set; }
        public bool IsActive { get; set; } = false;
        public UserLevel UserLevel { get; set; } = UserLevel.General;
        public bool EmailNotify { get; set; } = true;
        public bool LineNotify { get; set; } = false;
        public string? AccessToken { get; set; }
    }
}
