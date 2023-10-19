using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Name { get; set; } = null!;

        [Column(TypeName = "nvarchar(10)")]
        public string Name_zh { get; set; } = null!;
        public virtual List<Plan>? Plans { get; set; }
    }
}
