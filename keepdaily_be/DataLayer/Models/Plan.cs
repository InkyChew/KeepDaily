using DomainLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Plan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string Title { get; set; } = null!;
        [Column(TypeName = "nvarchar(50)")]
        public string Description { get; set; } = null!;
        public DateTime StartFrom { get; set; }
        public List<Day> Days { get; set; } = new List<Day>();
    }
}
