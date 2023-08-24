using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(LineId), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Column(TypeName = "nvarchar(10)")]
        public string Name { get; set; } = null!;
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; } = null!;
        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; } = null!;
        public bool EmailConfirmed { get; set; } = false;
        [Column(TypeName = "nvarchar(50)")]
        public string? LineId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? LineAccessToken { get; set; }
        public bool IsActive { get; set; } = false;
        public UserLevel Level { get; set; } = UserLevel.General;
        public NotifyType Notify { get; set; } = NotifyType.Email;
        public List<User> Friends { get; set; } = new List<User>();
        public List<Plan> Plans { get; set; } = new List<Plan>();
    }

    public enum UserLevel
    {
        General = 1,
        Premium = 2,
    }

    public enum NotifyType
    {
        None = 0,
        Email = 1,
        Line = 2,
        All = 3
    }
}
