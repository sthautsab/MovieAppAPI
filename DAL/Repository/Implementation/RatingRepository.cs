﻿using DAL.Repository.Interface;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Implementation
{
    public class RatingRepository : IRatingRepository
    {
        private readonly MovieContext _context;
        public RatingRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task AddRating(Rating rating)
        {
            Rating rate = new Rating();
            rate.UserId = rating.UserId;
            rate.MovieId = rating.MovieId;
            rate.Rate = rating.Rate;
            await _context.Ratings.AddAsync(rate);
            await _context.SaveChangesAsync();
        }

        public async Task<int?> GetRatingByUserIdAndMovieId(string UserId, Guid MovieId)
        {
            Rating rating = new Rating();
            rating = _context.Ratings.FirstOrDefault(r => r.UserId == UserId && r.MovieId == MovieId);
            if (rating != null)
            {
                return rating.Rate;
            }
            else
            {
                return 0;
            }
        }

        public async Task UpdateUserRating(Rating rating)
        {
            Rating existingRating = _context.Ratings.FirstOrDefault(x => x.UserId == rating.UserId && x.MovieId == rating.MovieId);
            existingRating.Rate = rating.Rate;

            _context.Ratings.Update(existingRating);
            await _context.SaveChangesAsync();
        }

        public async Task<double> GetAverageRating(Guid MovieId)
        {
            double averageRating = (await _context.Ratings
                .Where(r => r.MovieId == MovieId)
                .AverageAsync(r => r.Rate)).Value;

            return averageRating;

        }

        public Task<Rating> GetRatingDataByUserIdAndMovieId(string UserId, Guid MovieId)
        {
            throw new NotImplementedException();
        }
    }
}
