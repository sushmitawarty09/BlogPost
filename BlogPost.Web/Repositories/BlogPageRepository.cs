using BlogPost.Web.Data;
using BlogPost.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Web.Repositories
{
    public class BlogPageRepository : IBlogPageRepository
    {
        private readonly BlogPageDBContext blogPageDBContext;

        public BlogPageRepository(BlogPageDBContext blogPageDBContext)
        {
            this.blogPageDBContext = blogPageDBContext;
        }
        public async Task<BlogPage> AddAsync(BlogPage blogPage)
        {
            await blogPageDBContext.AddAsync(blogPage);
            await blogPageDBContext.SaveChangesAsync();
            return blogPage;
        }

        public async Task<BlogPage?> DeleteAsync(Guid id)
        {
            var blog = await blogPageDBContext.BlogPages.FindAsync(id);
            if(blog!=null)
            {
                blogPageDBContext.BlogPages.Remove(blog);
                await blogPageDBContext.SaveChangesAsync();
                return blog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPage>> GetAllAsync()
        {
            var blogpage=await blogPageDBContext.BlogPages.Include(x=> x.Tags).ToListAsync();
            return blogpage;
        }

        public Task<BlogPage?> GetAsync(Guid id)
        {
            return blogPageDBContext.BlogPages.Include(x=>x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPage?> GetBlogByUrl(string url)
        {
            return await blogPageDBContext.BlogPages.Include(x=>x.Tags).FirstOrDefaultAsync(x => x.URLHandler == url);
        }

        public async Task<BlogPage?> UpdateAsync(BlogPage blogPage)
        {
            var blog = await blogPageDBContext.BlogPages.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id==blogPage.Id);
            if(blog!=null)
            {
                blog.Id= blogPage.Id;
                blog.ImageURL=blogPage.ImageURL;
                blog.Author=blogPage.Author;
                blog.PublishedDate=blogPage.PublishedDate;
                blog.URLHandler=blogPage.URLHandler;
                blog.Content=blogPage.Content;
                blog.PublishedDate = blogPage.PublishedDate;
                blog.Description=blogPage.Description;
                blog.PageTitle=blogPage.PageTitle;
                blog.Visible=blogPage.Visible;
                blog.Haeding=blogPage.Haeding;
                blog.Tags=blogPage.Tags;

                await blogPageDBContext.SaveChangesAsync();
                return blogPage;
            }
            return null;
        }
    }
}
