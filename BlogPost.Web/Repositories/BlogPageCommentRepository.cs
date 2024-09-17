using BlogPost.Web.Data;
using BlogPost.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Web.Repositories
{
    public class BlogPageCommentRepository : IBlogPostCommentRepository
    {
        private readonly BlogPageDBContext blogPageDBContext;

        public BlogPageCommentRepository(BlogPageDBContext blogPageDBContext)
        {
            this.blogPageDBContext = blogPageDBContext;
        }
        public async Task<BlogPageComment> AddCommentAsync(BlogPageComment blogPageComment)
        {
            await blogPageDBContext.BlogComments.AddAsync(blogPageComment);
            await blogPageDBContext.SaveChangesAsync();
            return blogPageComment;
        }

        public async Task<IEnumerable<BlogPageComment>> GetCommentsByBlogId(Guid blogId)
        {
            return await blogPageDBContext.BlogComments.Where(x => x.BlogPageId == blogId).ToListAsync();
        }
    }
}
