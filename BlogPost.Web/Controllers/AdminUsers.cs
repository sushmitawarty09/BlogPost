using BlogPost.Web.Models.ViewModels;
using BlogPost.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminUsers : Controller
    {
        private readonly IusersRepository iusersRepository;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUsers(IusersRepository iusersRepository,UserManager<IdentityUser> userManager)
        {
            this.iusersRepository = iusersRepository;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users=await iusersRepository.GetAll();
            var us = new GetUsersRequest();
            us.Users = new List<UserRequest>();

            foreach (var user in users)
            {
                us.Users.Add(new Models.ViewModels.UserRequest
                {
                    EmailAddress = user.Email,
                    UserId = Guid.Parse(user.Id),
                    UserName = user.UserName
                });
            }
            return View(us);
        }
        [HttpPost]
        public async Task<IActionResult> List(GetUsersRequest getUsersRequest)
        {
            var identityUser = new IdentityUser
            {
                UserName = getUsersRequest.UserName,
                Email = getUsersRequest.Email
            };
            var result=await userManager.CreateAsync(identityUser, getUsersRequest.Password);
            if (result != null)
            {
              if (result.Succeeded)
                {
                    var roles = new List<string> { "User" };
                    if(getUsersRequest.UserCheck==true)
                    {
                        result=await userManager.AddToRoleAsync(identityUser, "Admin");
                        if(result.Succeeded && result!=null)
                        {
                            return RedirectToAction("List", "AdminUsers");
                        }
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await userManager.FindByIdAsync(id.ToString());
            if(result != null)
            {
                var user = await userManager.DeleteAsync(result);
                if (user.Succeeded && user != null)
                {
                    return RedirectToAction("List", "AdminUsers");
                }
            }
            return View();
        }
    }
}
