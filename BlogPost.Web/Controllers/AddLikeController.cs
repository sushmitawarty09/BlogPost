using BlogPost.Web.Models.Domain;
using BlogPost.Web.Models.ViewModels;
using BlogPost.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddLikeController : ControllerBase
    {
        private readonly IBlogPageLikeRepository blogPageLikeRepository;

        public AddLikeController(IBlogPageLikeRepository blogPageLikeRepository)
        {
            this.blogPageLikeRepository = blogPageLikeRepository;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest request)
        {
            var model = new BlogPageLike
            {
                BlogPageId = request.BlogPostId,
                Userid = request.UserId
            };
            await blogPageLikeRepository.AddLikes(model);
            return Ok();
        }

        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesByBlogId([FromRoute]Guid blogPostId)
        {
            var totalLikes=await blogPageLikeRepository.GetTotalLikes(blogPostId);
            return Ok(totalLikes);
        }
    }
}
