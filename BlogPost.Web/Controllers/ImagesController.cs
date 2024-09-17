using BlogPost.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogPost.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<IActionResult>Upload(IFormFile file)
        {
            var result= await imageRepository.UplaodAsync(file);
            if(result == null)
            {
                return Problem("Something Went Wrong!!", null,(int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = result });
        }
    }
}
