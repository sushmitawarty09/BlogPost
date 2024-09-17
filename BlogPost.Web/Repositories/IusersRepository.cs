using Microsoft.AspNetCore.Identity;

namespace BlogPost.Web.Repositories
{
    public interface IusersRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();


    }
}
