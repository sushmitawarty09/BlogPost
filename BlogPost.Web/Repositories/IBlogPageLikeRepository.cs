using BlogPost.Web.Models.Domain;

namespace BlogPost.Web.Repositories
{
    public interface IBlogPageLikeRepository
    {
        Task<int> GetTotalLikes(Guid id);
        Task<BlogPageLike> AddLikes(BlogPageLike request);
        Task<IEnumerable<BlogPageLike>> GetTotalLikesByUser(Guid blogPostId);
    }
}
