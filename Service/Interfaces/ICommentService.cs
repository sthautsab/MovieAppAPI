using DTO.MovieDTO;

namespace Service.Interfaces
{
    public interface ICommentService
    {
        Task AddComment(CommentVM commentVM);

        Task<List<CommentVM>> GetMovieComments(Guid movieId);

        Task<CommentVM> GetCommentByCommentId(Guid commentId);
        Task Edit(Guid commentId, CommentVM commentVM);

        Task Delete(Guid commentId);


    }
}
