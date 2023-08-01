using DAL.Repository.Interface;
using Domain.Models;
using DTO.MovieDTO;

namespace Service.Implementation
{
    public class RatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMovieRepository _movieRepository;

        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
        //AddUpdateRating
        public async Task AddRating(RatingVM ratingVM)
        {
            Rating rating = new Rating()
            {
                MovieId = ratingVM.MovieId,
                UserId = ratingVM.UserId,
                Rate = ratingVM.Rate
            };
            int? rate = await _ratingRepository.GetRatingByUserIdAndMovieId(rating.UserId, rating.MovieId);
            //rate is 0 if not rated 
            if (rate == 0)
            {
                await _ratingRepository.AddRating(rating);
            }
            else
            {
                await _ratingRepository.UpdateUserRating(rating);
            }
            double averageRating = await _ratingRepository.GetAverageRating(ratingVM.MovieId);

            var movie = await _movieRepository.GetMovieById(rating.MovieId);
            if (movie != null)
            {
                movie.AverageRating = averageRating;
                await _movieRepository.UpdateMovie(movie);
            }
        }

        public async Task<int?> GetGetRatingByMovieAndUserId(string userId, Guid movieId)
        {
            int? rate = await _ratingRepository.GetRatingByUserIdAndMovieId(userId, movieId);
            return rate;
        }
    }
}
