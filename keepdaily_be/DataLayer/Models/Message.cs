﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string Title { get; set; } = null!;
        [Column(TypeName = "nvarchar(100)")]
        public string Content { get; set; } = null!;
        [Column(TypeName = "nvarchar(100)")]
        public string? Link { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? Image { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
    }
}
