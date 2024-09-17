using BlogPost.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogPost.Web.Models.ViewModels
{
    public class AddBlogPageRequest
    {
        public string Haeding { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string URLHandler { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        public IEnumerable<SelectListItem> Tags { get; set; }
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
