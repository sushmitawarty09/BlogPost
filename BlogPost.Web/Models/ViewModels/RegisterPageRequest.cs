using System.ComponentModel.DataAnnotations;

namespace BlogPost.Web.Models.ViewModels
{
    public class RegisterPageRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(6,ErrorMessage ="Password should be minimun 6 characters")]
        public string Password { get; set; }
    }
}
