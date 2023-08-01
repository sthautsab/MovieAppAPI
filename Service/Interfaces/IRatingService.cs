using DTO.MovieDTO;

namespace Service.Interfaces
{
    public interface IRatingService
    {
        Task AddRating(RatingVM ratingVM);

        Task<int?> GetRatingByMovieAndUserId(string userId, Guid movieId);
    }
}
