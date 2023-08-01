using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DTO.MovieDTO
{
    public class MovieVM
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        [Required]
        public string? Genre { get; set; }

        public string? Director { get; set; }
        public string? Description { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? Photo { get; set; }

        public Double AverageRating { get; set; }
        public string? PhotoPath { get; set; }
    }
}
