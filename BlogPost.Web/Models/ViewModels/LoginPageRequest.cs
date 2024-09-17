using System.ComponentModel.DataAnnotations;

namespace BlogPost.Web.Models.ViewModels
{
    public class LoginPageRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MinLength(6,ErrorMessage ="Min length is 6 characters")]
        public string Password { get; set; }
        public string? UrlRequest { get; set; }
    }
}
