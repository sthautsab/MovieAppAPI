namespace DTO.MovieDTO
{
    public class PagedMovieVM
    {
        public List<MovieVM> Movies { get; set; }
        public MovieVM MovieVM { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalMovies { get; set; }
        public int TotalPages { get; set; }
    }
}
