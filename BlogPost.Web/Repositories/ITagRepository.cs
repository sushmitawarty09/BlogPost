using BlogPost.Web.Models.Domain;
using System.ComponentModel;

namespace BlogPost.Web.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync(string? queryString=null,string? sortBy=null,string? sortDirection=null);

        Task<Tag?> GetAsync(Guid id);

        Task<Tag> AddAsync(Tag tag);

        Task<Tag?> UpdateAsync(Tag tag);

        Task<Tag?> DeleteAsync(Guid id);
    }
}
