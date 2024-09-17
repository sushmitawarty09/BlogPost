using BlogPost.Web.Models.Domain;

namespace BlogPost.Web.Repositories
{
    public interface IBlogPageRepository
    {
        Task<IEnumerable<BlogPage>> GetAllAsync();
        Task<BlogPage?> GetAsync(Guid id);
        Task<BlogPage> AddAsync(BlogPage blogPage);
        Task<BlogPage?> UpdateAsync(BlogPage blogPage);
        Task<BlogPage?> DeleteAsync(Guid id);

        Task<BlogPage?> GetBlogByUrl(string url);
    }
}
