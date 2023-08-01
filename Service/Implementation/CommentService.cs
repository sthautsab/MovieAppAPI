using DAL.Repository.Interface;
using Domain.Models;
using DTO.MovieDTO;
using Service.Interfaces;

namespace Service.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task AddComment(CommentVM commentVM)
        {
            Comment comment = new Comment()
            {
                Content = commentVM.Content,
                UserName = commentVM.UserName,
                DatePosted = DateTime.Now,
                MovieId = commentVM.MovieId
            };
            await _commentRepository.AddComment(comment);
        }

        public async Task<CommentVM> GetCommentByCommentId(Guid commentId)
        {
            Comment comment = await _commentRepository.GetCommentById(commentId);
            CommentVM commentVM = new CommentVM()
            {
                Content = comment.Content,
                UserName = comment.UserName,
                DatePosted = DateTime.Now,
                MovieId = comment.MovieId,
                CommentId = comment.CommentId
            };
            return commentVM;
        }

        public async Task<List<CommentVM>> GetMovieComments(Guid movieId)
        {
            List<Comment>? comments = await _commentRepository.GetMovieComments(movieId) ?? null;

            List<CommentVM> commentsVM = new List<CommentVM>();

            commentsVM = comments.Select(x => new CommentVM()
            {
                Content = x.Content,
                UserName = x.UserName,
                DatePosted = x.DatePosted,
                MovieId = x.MovieId,
                CommentId = x.CommentId
            }).ToList();

            return commentsVM;
        }

        public async Task Edit(Guid commentId, CommentVM commentVM)
        {
            Comment existingComment = await _commentRepository.GetCommentById(commentId);

            if (existingComment != null)
            {
                existingComment.Content = commentVM.Content;
                existingComment.UserName = commentVM.UserName;
                existingComment.DatePosted = DateTime.Now;

                await _commentRepository.UpdateComment(existingComment);
            }
        }

        public async Task Delete(Guid commentId)
        {
            await _commentRepository.DeleteComment(commentId);
        }

        public Task Edit(Guid commentId)
        {
            throw new NotImplementedException();
        }
    }
}
