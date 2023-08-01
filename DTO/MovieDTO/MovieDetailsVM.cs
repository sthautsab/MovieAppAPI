namespace DTO.MovieDTO
{
    public class MovieDetailsVM
    {
        public MovieVM MovieVM { get; set; }
        public List<CommentVM> comments { get; set; }
    }
}
