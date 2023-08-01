using Domain.Models;

namespace DAL.Repository.Interface
{
    public interface IRatingRepository
    {
        Task AddRating(Rating rating);
        Task<int?> GetRatingByUserIdAndMovieId(string UserId, Guid MovieId);

        Task<Rating> GetRatingDataByUserIdAndMovieId(string UserId, Guid MovieId);
        Task UpdateUserRating(Rating rating);
        Task<double> GetAverageRating(Guid MovieId);
    }
}
