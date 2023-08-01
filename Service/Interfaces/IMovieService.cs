using DTO.MovieDTO;
using Microsoft.AspNetCore.Http;

namespace Service.Interfaces
{
    public interface IMovieService
    {
        Task AddMovie(MovieVM movieVM);

        Task<List<MovieVM>> GetMovies();
        Task<PagedMovieVM> GetPagedMovies(int? page);

        Task<MovieVM> GetMovieById(Guid id);

        Task<MovieUpdateVM> GetMovieDataForEdit(Guid id);


        Task<MovieDetailsVM> GetMovieDetails(Guid id);

        Task EditMovie(MovieUpdateVM movieUpdateVM);

        Task DeleteMovie(Guid id);

        Task<string> UploadFile(IFormFile file);
    }
}
