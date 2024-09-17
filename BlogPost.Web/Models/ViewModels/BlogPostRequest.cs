using BlogPost.Web.Models.Domain;

namespace BlogPost.Web.Models.ViewModels
{
    public class BlogPostRequest
    {

        public Guid Id { get; set; }
        public string Haeding { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string URLHandler { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public int TotalLikes { get; set; }
        public bool Liked { get; set; }
        public string Comments { get; set; }

        public IEnumerable<BlogComments> BlogComments { get; set; }
    }
}
