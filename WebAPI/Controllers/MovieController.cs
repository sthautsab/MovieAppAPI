using Domain.Models;
using DTO.MovieDTO;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ICommentService _commentService;
        public MovieController(IMovieService movieService, ICommentService commentService)
        {
            _movieService = movieService;
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            try
            {
                List<MovieVM> moviesVM = await _movieService.GetMovies();
                if (moviesVM.Count != 0)
                {
                    ResponseMessage<List<MovieVM>> responseMessage = new ResponseMessage<List<MovieVM>>()
                    {
                        Success = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Data = moviesVM
                    };
                    return Ok(responseMessage);
                }
                else
                {
                    ResponseMessage<string> responseMessage = new ResponseMessage<string>()
                    {
                        Success = false,
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        Data = "No Record Found"
                    };
                    return NotFound(responseMessage);
                }
            }
            catch (Exception ex)
            {
                ResponseMessage<string> responseMessage = new ResponseMessage<string>()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Data = ex.Message
                };
                return BadRequest(responseMessage);
            }
        }
        [HttpGet]
        [Route("GetMovieById")]
        public async Task<ResponseMessage<MovieDetailsVM>> GetMoviesById(Guid movieId)
        {
            try
            {
                MovieDetailsVM movieDetails = new MovieDetailsVM();

                MovieVM movieVM = await _movieService.GetMovieById(movieId);

                List<CommentVM>? commentsVM = await _commentService.GetMovieComments(movieId);

                movieDetails.MovieVM = movieVM;
                movieDetails.comments = commentsVM;
                if (movieVM != null)
                {
                    return new ResponseMessage<MovieDetailsVM> { Success = true, StatusCode = System.Net.HttpStatusCode.OK, Data = movieDetails };
                }
                else
                {
                    return new ResponseMessage<MovieDetailsVM> { Success = false, StatusCode = System.Net.HttpStatusCode.NotFound };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage<MovieDetailsVM> { Success = false, StatusCode = System.Net.HttpStatusCode.BadRequest };
            }
        }

        [HttpPost]
        [Route("AddMovie")]
        public async Task<ResponseMessage<MovieVM>> AddMovie(MovieVM movieVM)
        {
            try
            {

                _movieService.AddMovie(movieVM);
                return new ResponseMessage<MovieVM> { Success = true, StatusCode = System.Net.HttpStatusCode.OK, Data = movieVM };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<MovieVM> { Success = false, StatusCode = System.Net.HttpStatusCode.BadRequest };
            }
        }

        [HttpPost]
        public async Task<ResponseMessage<MovieUpdateVM>> EditMovie(MovieUpdateVM movieUpdateVM)
        {
            try
            {
                _movieService.EditMovie(movieUpdateVM);
                return new ResponseMessage<MovieUpdateVM> { Success = true, StatusCode = System.Net.HttpStatusCode.OK, Data = movieUpdateVM, Message = "Movie Updated Successfully" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<MovieUpdateVM> { Success = false, StatusCode = System.Net.HttpStatusCode.BadRequest };
            }
        }

        [HttpDelete]
        public async Task<ResponseMessage<string>> Delete(Guid movieId)
        {
            try
            {
                _movieService.DeleteMovie(movieId);
                return new ResponseMessage<string> { Success = true, StatusCode = System.Net.HttpStatusCode.OK, Message = "Movie Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<string> { Success = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Movie Deletion Failed" };
            }
        }

    }
}
