using System.ComponentModel.DataAnnotations;

namespace DTO.MovieDTO
{
    public class RatingVM
    {
        public string UserId { get; set; }
        public Guid MovieId { get; set; }
        [Range(0, 5)]
        public int? Rate { get; set; }
    }
}
