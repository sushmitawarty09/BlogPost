using BlogPost.Web.Data;
using BlogPost.Web.Models.Domain;
using BlogPost.Web.Models.ViewModels;
using BlogPost.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagController : Controller
    {
        private ITagRepository _tagRepository;
        public AdminTagController(ITagRepository tagRepository)
        {
            this._tagRepository = tagRepository;
        }

        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            Validateforerrors(addTagRequest);
            if (ModelState.IsValid)
            {
                var tag = new Tag
                {
                    Name = addTagRequest.Name,
                    DisplayName = addTagRequest.DisplayName
                };
                await _tagRepository.AddAsync(tag);
                return RedirectToAction("List");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List(string? QuertString,string? sortBy,string? sortDirection)
        {
            var tags = await _tagRepository.GetAllAsync(QuertString,sortBy,sortDirection);
            ViewBag.searchQuery = QuertString;
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var tag = await _tagRepository.GetAsync(id);
            if(tag!=null)
            {
                var editTagRequest = new EditTagrequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(EditTagrequest editTagrequest)
        {
            var tag = new Tag
            {
                Id=editTagrequest.Id,
                Name = editTagrequest.Name,
                DisplayName = editTagrequest.DisplayName
            };
            var updatedTag = await _tagRepository.UpdateAsync(tag);
            if(updatedTag!=null)
            {
               //show success notification
            }
            else
            {
                //show error notification
            }

            return RedirectToAction("Edit", new { id = editTagrequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(EditTagrequest editTagrequest)
        {
            var deletedtag = await _tagRepository.DeleteAsync(editTagrequest.Id);
            if(deletedtag!=null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editTagrequest.Id });
        }

        private void Validateforerrors(AddTagRequest request)
        {
            if(request.Name !=null && request.DisplayName != null)
            {
                if(request.Name==request.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name cannot be same as display name");
                }
            }
        }
    }
}
