using BlogPost.Web.Models.Domain;

namespace BlogPost.Web.Models.ViewModels
{
    public class HomePageRequest
    {
        public IEnumerable<BlogPage> Blog { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
