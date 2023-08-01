using Domain.Models;
using DTO.MovieDTO;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        [HttpPost]
        [Route("AddOrUpdateRating")]
        public async Task<ResponseMessage<RatingVM>> AddUpdateRating(RatingVM ratingVM)
        {
            try
            {
                await _ratingService.AddRating(ratingVM);
                return new ResponseMessage<RatingVM> { Success = true, StatusCode = System.Net.HttpStatusCode.OK, Message = "Rated Successfully", Data = ratingVM };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<RatingVM> { Success = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message };
            }
        }

        [HttpGet]
        [Route("GetRatingByUserIdAndMovieId")]
        public async Task<ResponseMessage<int?>> GetRating(string userId, Guid movieId)
        {
            try
            {
                int? rate = await _ratingService.GetRatingByMovieAndUserId(userId, movieId);
                return new ResponseMessage<int?> { Success = true, StatusCode = System.Net.HttpStatusCode.OK, Message = "Rated Data Retrived", Data = rate };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<int?> { Success = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message };
            }
        }
    }
}
