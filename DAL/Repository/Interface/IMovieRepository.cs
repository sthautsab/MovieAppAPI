using Domain.Models;

namespace DAL.Repository.Interface
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetMovies();
        Task<Movie> GetMovieById(Guid id);
        Task<bool> AddMovie(Movie model);
        Task<bool> UpdateMovie(Movie updatedMovie);
        bool DeleteMovie(Guid id);
    }
}
