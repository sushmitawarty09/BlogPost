using BlogPost.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterPageRequest registerPageRequest)
        {
            if(ModelState.IsValid)
            {
                var identityUser = new IdentityUser
                {
                    UserName = registerPageRequest.Username,
                    Email = registerPageRequest.Email
                };

                var identityResult = await userManager.CreateAsync(identityUser, registerPageRequest.Password);
                if (identityResult.Succeeded)
                {
                    var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");
                    if (roleIdentityResult.Succeeded)
                    {
                        return RedirectToAction("Register");
                    }
                }
            }
           
            return View("Register");
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginPageRequest
            {
                UrlRequest = ReturnUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginPageRequest loginPage)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var signInResult = await signInManager.PasswordSignInAsync(loginPage.UserName, loginPage.Password, false, false);
            if(signInResult.Succeeded && signInResult!=null)
            {
                if(!string.IsNullOrWhiteSpace(loginPage.UrlRequest))
                {
                   return Redirect(loginPage.UrlRequest);
                }
                return RedirectToAction("Index", "Home");
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
