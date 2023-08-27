using DomainLayer.Models;
namespace DomainLayer.Dto
{
    public class VUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
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
    }
}
