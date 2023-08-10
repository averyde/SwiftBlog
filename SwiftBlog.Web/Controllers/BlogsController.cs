using Microsoft.AspNetCore.Mvc;
using SwiftBlog.Web.Models.ViewModels;
using SwiftBlog.Web.Repositories;

namespace SwiftBlog.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
		private readonly ILikeRepository likeRepository;

		public BlogsController(IBlogPostRepository blogPostRepository, ILikeRepository likeRepository)
        {
            this.blogPostRepository = blogPostRepository;
			this.likeRepository = likeRepository;
		}

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);

			var blogDetailsViewModel = new BlogDetailsViewModel();

            if (blogPost != null)
            {
                var totalLikes = await likeRepository.GetTotalLikes(blogPost.Id);

				blogDetailsViewModel = new BlogDetailsViewModel
				{
					Id = blogPost.Id,
					Heading = blogPost.Heading,
					PageTitle = blogPost.PageTitle,
					Content = blogPost.Content,
					ShortDescription = blogPost.ShortDescription,
					FeaturedImageUrl = blogPost.FeaturedImageUrl,
					UrlHandle = blogPost.UrlHandle,
					PublishedDate = blogPost.PublishedDate,
					Author = blogPost.Author,
					Visible = blogPost.Visible,
					Tags = blogPost.Tags,
					TotalLikes = totalLikes,
				};

				return View(blogDetailsViewModel);
			}

            return View(blogDetailsViewModel);
        }
    }
}
