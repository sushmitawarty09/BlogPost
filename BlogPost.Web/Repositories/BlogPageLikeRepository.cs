using BlogPost.Web.Data;
using BlogPost.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Web.Repositories
{
    public class BlogPageLikeRepository : IBlogPageLikeRepository
    {
        private readonly BlogPageDBContext blogPageDBContext;

        public BlogPageLikeRepository(BlogPageDBContext blogPageDBContext)
        {
            this.blogPageDBContext = blogPageDBContext;
        }

        public async Task<BlogPageLike> AddLikes(BlogPageLike request)
        {
             await blogPageDBContext.BlogLikes.AddAsync(request);
            await blogPageDBContext.SaveChangesAsync();
            return request;
        }

        public async Task<int> GetTotalLikes(Guid id)
        {
            return await blogPageDBContext.BlogLikes.CountAsync(x => x.BlogPageId == id);
        }

        public async Task<IEnumerable<BlogPageLike>> GetTotalLikesByUser(Guid blogPostId)
        {
            return await blogPageDBContext.BlogLikes.Where(x=>x.BlogPageId==blogPostId).ToListAsync();
        }
    }
}
