using System.ComponentModel.DataAnnotations;

namespace DTO.MovieDTO
{
    public class CommentVM
    {
        public Guid CommentId { get; set; }
        public Guid MovieId { get; set; }
        public string? UserName { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
