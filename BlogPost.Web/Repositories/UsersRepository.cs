using BlogPost.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Web.Repositories
{
    public class UsersRepository : IusersRepository
    {
        private readonly BlogPageAuthDBContext blogPageAuthDBContext;

        public UsersRepository(BlogPageAuthDBContext blogPageAuthDBContext)
        {
            this.blogPageAuthDBContext = blogPageAuthDBContext;
        }

       
        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users=await blogPageAuthDBContext.Users.ToListAsync();
            var adminUser = await blogPageAuthDBContext.Users.FirstOrDefaultAsync(x => x.Email == "SuperAdmin@test.com");
            if (adminUser != null)
            {
                users.Remove(adminUser);
            }
            return users;
        }
    }
}
