using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        [Column(TypeName = "nvarchar(100)")]
        public string? Password { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Description { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? ImgName { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string? ImgType { get; set; }
        public bool EmailConfirmed { get; set; } = false;
        [Column(TypeName = "nvarchar(50)")]
        public string? LineId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? LineAccessToken { get; set; }
        public bool IsActive { get; set; } = false;
        public UserLevel UserLevel { get; set; } = UserLevel.General;
        public bool EmailNotify { get; set; } = true;
        public bool LineNotify { get; set; } = false;
        public virtual List<Plan> Plans { get; set; } = new List<Plan>();
    }

    public enum UserLevel
    {
        Admin,
        General,
        Premium
    }
}
