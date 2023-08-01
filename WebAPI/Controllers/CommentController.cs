using Domain.Models;
using DTO.MovieDTO;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Route("AddComment")]
        public async Task<ResponseMessage<CommentVM>> AddComment(CommentVM commentVM)
        {
            try
            {
                await _commentService.AddComment(commentVM);
                return new ResponseMessage<CommentVM> { Success = true, StatusCode = System.Net.HttpStatusCode.OK, Message = "Comment Added Successfully", Data = commentVM };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<CommentVM> { Success = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message };
            }
        }

        [HttpGet]
        [Route("GetCommentsByMovieId")]

        public async Task<ResponseMessage<List<CommentVM>>> GetMovieComments(Guid movieId)
        {
            try
            {
                List<CommentVM> commentsVM = await _commentService.GetMovieComments(movieId);
                if (commentsVM.Count > 0)
                {
                    return new ResponseMessage<List<CommentVM>> { Success = true, StatusCode = System.Net.HttpStatusCode.OK, Data = commentsVM };
                }
                else
                {
                    return new ResponseMessage<List<CommentVM>> { Success = false, StatusCode = System.Net.HttpStatusCode.NoContent, Message = "No Comments Found" };

                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage<List<CommentVM>> { Success = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message };

            }
        }

        [HttpPost]
        [Route("EditComment")]
        public async Task<ResponseMessage<CommentVM>> EditComment(Guid commentId, CommentVM commentVM)
        {
            try
            {
                CommentVM comVM = new CommentVM();
                comVM = await _commentService.GetCommentByCommentId(commentId);

                if (comVM != null)
                {
                    await _commentService.Edit(commentId, commentVM);
                    return new ResponseMessage<CommentVM> { Success = true, StatusCode = System.Net.HttpStatusCode.OK, Message = "Comment Edited", Data = commentVM };
                }
                else
                {
                    return new ResponseMessage<CommentVM> { Success = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Error" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage<CommentVM> { Success = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message };
            }
        }

        [HttpPost]
        [Route("DeleteComment")]
        public async Task<ResponseMessage<String>> Delete(Guid commentId)
        {
            try
            {
                await _commentService.Delete(commentId);
                return new ResponseMessage<String> { Success = true, StatusCode = System.Net.HttpStatusCode.OK, Message = "Comment Deleted" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<String> { Success = false, StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Comment Deletion Failed" };
            }
        }
    }
}
