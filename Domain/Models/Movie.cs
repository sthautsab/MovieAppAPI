﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Genre { get; set; }

        public string? Director { get; set; }
        public string? Description { get; set; }

        public string? PhotoPath { get; set; }

        [DefaultValue(0)]
        public double AverageRating { get; set; }

        //public ICollection<Comment> Comments { get; set; }
    }
}
