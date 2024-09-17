using BlogPost.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BlogPost.Web.Data
{
    public class BlogPageDBContext : DbContext
    {
        public BlogPageDBContext(DbContextOptions<BlogPageDBContext> options) : base(options)
        {
        }
        public DbSet<BlogPage> BlogPages { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<BlogPageComment> BlogComments { get; set; }
        public DbSet<BlogPageLike> BlogLikes { get; set; }
    }
}
