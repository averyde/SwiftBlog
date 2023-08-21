using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwiftBlog.Web.Models.Domain;
using SwiftBlog.Web.Models.ViewModels;
using SwiftBlog.Web.Repositories;

namespace SwiftBlog.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

		[HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // map AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
                Color = addTagRequest.Color,
            };

            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

		[HttpGet]
        public async Task<IActionResult> List()
        {
            // use dbContext to read the tags
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }

		[HttpGet]
        public async Task<IActionResult> Edit(Guid id) 
        {
            var tag = await tagRepository.GetAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                    Color = tag.Color,
                };

                return View(editTagRequest);
            }
            return View(null);
        }

		[HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
                Color = editTagRequest.Color,
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            {
                return RedirectToAction("List");
            }

            // fail
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

		[HttpGet]
        public async Task<IActionResult> Delete(Guid id) 
        {
            var deletedTag = await tagRepository.DeleteAsync(id);
            if (deletedTag != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("List");
        }
    }
}
