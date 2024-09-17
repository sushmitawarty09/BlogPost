using BlogPost.Web.Data;
using BlogPost.Web.Models.Domain;
using BlogPost.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogPageDBContext blogPageDBContext;

        public TagRepository(BlogPageDBContext blogPageDBContext)
        {
            this.blogPageDBContext = blogPageDBContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await blogPageDBContext.Tags.AddAsync(tag);
            await blogPageDBContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag=await blogPageDBContext.Tags.FindAsync(id);
            if(existingTag!=null)
            {
                blogPageDBContext.Tags.Remove(existingTag);
                await blogPageDBContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(string? queryString, string? sortBy, string? sortDirection)
        {
            var query = blogPageDBContext.Tags.AsQueryable();
            if(string.IsNullOrWhiteSpace(queryString)==false)
            {
                query = query.Where(x => x.Name.Contains(queryString) || x.DisplayName.Contains(queryString));
            }
            if(string.IsNullOrWhiteSpace(sortBy)==false)
            {
                var isDesc = string.Equals(sortDirection, "Des", StringComparison.OrdinalIgnoreCase);
                if(string.Equals(sortBy,"Name",StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                }
                if(string.Equals(sortBy,"DisplayName",StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.DisplayName) : query.OrderBy(x => x.DisplayName);
                }
            }

            return await query.ToListAsync();
        }

        public  Task<Tag?> GetAsync(Guid id)
        {
            return blogPageDBContext.Tags.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag=await blogPageDBContext.Tags.FindAsync(tag.Id);
            if(existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await blogPageDBContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
