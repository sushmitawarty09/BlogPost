using BlogPost.Web.Models.Domain;

namespace BlogPost.Web.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPageComment> AddCommentAsync(BlogPageComment blogPageComment);
        Task<IEnumerable<BlogPageComment>> GetCommentsByBlogId(Guid blogId);
    }
}
