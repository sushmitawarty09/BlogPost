using BlogPost.Web.Models.Domain;
using BlogPost.Web.Models.ViewModels;
using BlogPost.Web.Repositories;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogPageRepository blogPage;
        private readonly IBlogPageLikeRepository blogPageLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBlogPostCommentRepository blogPostCommentRepository;

        public BlogController(IBlogPageRepository blogPage,IBlogPageLikeRepository blogPageLikeRepository,
            SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager,
            IBlogPostCommentRepository blogPostCommentRepository)
        {
            this.blogPage = blogPage;
            this.blogPageLikeRepository = blogPageLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.blogPostCommentRepository = blogPostCommentRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string url)
        {
            var blog = await blogPage.GetBlogByUrl(url);
            var likes = false;
            var blogPostViewModel = new BlogPostRequest();
            if(blog!= null)
            {
                int totalLikes = await blogPageLikeRepository.GetTotalLikes(blog.Id);
                if (signInManager.IsSignedIn(User))
                {
                    var likesForBlog=await blogPageLikeRepository.GetTotalLikesByUser(blog.Id);
                    var userId = userManager.GetUserId(User);
                    if (userId != null)
                    {
                        var likesforUser=likesForBlog.FirstOrDefault(x => x.Userid == Guid.Parse(userId));
                        likes = likesforUser != null;
                    }
                }
                var blogComment = await blogPostCommentRepository.GetCommentsByBlogId(blog.Id);
                var blogCommentsForView = new List<BlogComments>();
                foreach (var comment in blogComment)
                {
                    blogCommentsForView.Add(new BlogComments
                    {
                        CommentDescription = comment.Description,
                        DateAdded = comment.DateAdded,
                        UserName = (await userManager.FindByIdAsync(comment.UserId.ToString())).UserName
                    }) ;
                }
                
                blogPostViewModel = new BlogPostRequest
                {
                    Id = blog.Id,
                    Author = blog.Author,
                    PublishedDate = blog.PublishedDate,
                    Visible = blog.Visible,
                    Content = blog.Content,
                    Description = blog.Description,
                    Haeding = blog.Haeding,
                    ImageURL = blog.ImageURL,
                    PageTitle = blog.PageTitle,
                    Tags = blog.Tags,
                    URLHandler = blog.URLHandler,
                    TotalLikes = totalLikes,
                    Liked=likes,
                    BlogComments = blogCommentsForView
                };


            }
            return View(blogPostViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(BlogPostRequest blogPostRequest)
        {
            if (signInManager.IsSignedIn(User))
            {
                var model = new BlogPageComment
                {
                    BlogPageId = blogPostRequest.Id,
                    Description = blogPostRequest.Comments,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
                };
                await blogPostCommentRepository.AddCommentAsync(model);
                return RedirectToAction("Index", "Blog", new { url = blogPostRequest.URLHandler });
            }
            return View();
        }

       
    }
}
