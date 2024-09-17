namespace BlogPost.Web.Models.ViewModels
{
    public class GetUsersRequest
    {
        public List<UserRequest> Users { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool UserCheck { get; set; }
    }
}
