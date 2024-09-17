using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPost.Web.Models.Domain
{
    public class BlogPageComment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
      
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid BlogPageId { get; set; }
    }
}
