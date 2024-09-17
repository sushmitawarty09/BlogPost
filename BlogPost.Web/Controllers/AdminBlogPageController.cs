using BlogPost.Web.Models.Domain;
using BlogPost.Web.Models.ViewModels;
using BlogPost.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogPost.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPageController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPageRepository blogPageRepository;

        public AdminBlogPageController(ITagRepository tagRepository,IBlogPageRepository blogPageRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPageRepository = blogPageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tag=await tagRepository.GetAllAsync();
            var model = new AddBlogPageRequest
            {
                Tags =tag.Select(x=>new SelectListItem { Text=x.Name, Value=x.Id.ToString()})
            };
            return View(model);
        }

       [HttpPost]
        public async Task<IActionResult> Add(AddBlogPageRequest addBlogPageRequest)
        {
            var blogpage = new BlogPage
            {
                Haeding = addBlogPageRequest.Haeding,
                Content = addBlogPageRequest.Content,
                Description = addBlogPageRequest.Description,
                PublishedDate = addBlogPageRequest.PublishedDate,
                ImageURL = addBlogPageRequest.ImageURL,
                URLHandler = addBlogPageRequest.URLHandler,
                Author = addBlogPageRequest.Author,
                Visible = addBlogPageRequest.Visible,
                PageTitle = addBlogPageRequest.PageTitle
            };

            var selectedTag = new List<Tag>();
            foreach(var selectedTagId in addBlogPageRequest.SelectedTags)
            {
                var tag = Guid.Parse(selectedTagId);
                var existingTag=  await tagRepository.GetAsync(tag);
                if(existingTag != null)
                {
                    selectedTag.Add(existingTag);
                }

            }
            blogpage.Tags = selectedTag;
            await blogPageRepository.AddAsync(blogpage);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogpost = await blogPageRepository.GetAllAsync();
            return View(blogpost);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blogpost = await blogPageRepository.GetAsync(id);
            var tags = await tagRepository.GetAllAsync();
            if(blogpost!=null)
            {
                var blog = new EditBlogPageRequest()
                {
                    Description=blogpost.Description,
                    Author=blogpost.Author,
                    Visible = blogpost.Visible,
                    PageTitle = blogpost.PageTitle,
                    ImageURL = blogpost.ImageURL,
                    URLHandler = blogpost.URLHandler,
                    Content = blogpost.Content,
                    PublishedDate = blogpost.PublishedDate,
                    Haeding=blogpost.Haeding,
                    Id=blogpost.Id,
                    Tags=tags.Select(x=>new SelectListItem { Text=x.Name , Value = x.Id.ToString()}),
                    SelectedTags=blogpost.Tags.Select(x=>x.Id.ToString()).ToArray()
                };

                return View(blog);  
            }
            return View(null);

        }

        [HttpPost]
        public async Task<IActionResult> Update(EditBlogPageRequest editBlogPageRequest)
        {
            var blog = new BlogPage
            {
                Id = editBlogPageRequest.Id,
                Author = editBlogPageRequest.Author,
                Visible = editBlogPageRequest.Visible,
                Content = editBlogPageRequest.Content,
                Description = editBlogPageRequest.Description,
                Haeding = editBlogPageRequest.Haeding,
                ImageURL = editBlogPageRequest.ImageURL,
                PageTitle = editBlogPageRequest.PageTitle,
                PublishedDate = editBlogPageRequest.PublishedDate,
                URLHandler = editBlogPageRequest.URLHandler,
            };
            var selectedTags = new List<Tag>();
            foreach(var selectedTag in editBlogPageRequest.SelectedTags)
            {
                if(Guid.TryParse(selectedTag, out var tag))
                {
                    var foundtag = await tagRepository.GetAsync(tag);
                    if(foundtag != null)
                    {
                        selectedTags.Add(foundtag);
                    }
                }
            }
            blog.Tags = selectedTags;
            var updatedBlog = await blogPageRepository.UpdateAsync(blog);
            if(updatedBlog != null)
            {
                return RedirectToAction("Edit",new {id= updatedBlog.Id} );
            }

            return RedirectToAction("Edit", new { id = editBlogPageRequest.Id });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPageRequest editBlogPageRequest)
        {
            var blog = await blogPageRepository.DeleteAsync(editBlogPageRequest.Id);
            if (blog!=null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit",new EditBlogPageRequest { Id=editBlogPageRequest.Id});
        }
    }
}
