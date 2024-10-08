﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Plan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Title { get; set; } = null!;
        [Column(TypeName = "nvarchar(50)")]
        public string Description { get; set; } = null!;
        [Column(TypeName = "nvarchar(16)")]
        public string StartFrom { get; set; } = null!;
        public DateTime UpdateTime { get; set; }
        public virtual List<Day> Days { get; set; } = new List<Day>();
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
