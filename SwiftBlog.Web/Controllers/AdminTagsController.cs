using Microsoft.AspNetCore.Mvc;
using SwiftBlog.Web.Data;
using SwiftBlog.Web.Models.Domain;
using SwiftBlog.Web.Models.ViewModels;

namespace SwiftBlog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly SwiftBlogDbContext _swiftBlogDbContext;

        public AdminTagsController(SwiftBlogDbContext swiftBlogDbContext)
        {
            _swiftBlogDbContext = swiftBlogDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            // map AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
                Color = addTagRequest.Color,
            };

            _swiftBlogDbContext.Tags.Add(tag);
            _swiftBlogDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List()
        {
            // use dbContext to read the tags
            var tags = _swiftBlogDbContext.Tags.ToList();

            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id) 
        {
            var tag = _swiftBlogDbContext.Tags.FirstOrDefault(x => x.Id == id);

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
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
                Color = editTagRequest.Color,
            };

            var existingTag = _swiftBlogDbContext.Tags.Find(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                existingTag.Color = tag.Color;

                _swiftBlogDbContext.SaveChanges();

                // success
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }

            // fail
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        public IActionResult Delete(Guid id) {
            var tag = _swiftBlogDbContext.Tags.Find(id);

            if (tag != null)
            {
                _swiftBlogDbContext.Tags.Remove(tag);
                _swiftBlogDbContext.SaveChanges();

                return RedirectToAction("List");
            }

            return RedirectToAction("List");
        }
    }
}
