using Microsoft.AspNetCore.Mvc;
using SwiftBlog.Web.Data;
using SwiftBlog.Web.Models.Domain;
using SwiftBlog.Web.Models.ViewModels;

namespace SwiftBlog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private SwiftBlogDbContext _swiftBlogDbContext;

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
            //Map AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
                Color = addTagRequest.Color,
            };

            _swiftBlogDbContext.Tags.Add(tag);
            _swiftBlogDbContext.SaveChanges();

            return View("Add");
        }
    }
}
