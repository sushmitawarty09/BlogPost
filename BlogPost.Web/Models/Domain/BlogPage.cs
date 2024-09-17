namespace BlogPost.Web.Models.Domain
{
    public class BlogPage
    {
        public Guid Id { get; set; }
        public string Haeding { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string URLHandler { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set;}
        public bool Visible { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<BlogPageComment> Comments { get; set; }
        public ICollection<BlogPageLike> Likes { get; set; }
    }
}
