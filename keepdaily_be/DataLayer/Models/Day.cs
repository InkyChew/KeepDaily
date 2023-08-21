using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Day
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Date { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string ImgUrl { get; set; } = null!;
        public int PlanId { get; set; }
    }
}
