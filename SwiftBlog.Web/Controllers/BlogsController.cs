using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SwiftBlog.Web.Models.ViewModels;
using SwiftBlog.Web.Repositories;

namespace SwiftBlog.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
		private readonly IBlogPostLikeRepository likeRepository;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly UserManager<IdentityUser> userManager;

		public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository likeRepository, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.blogPostRepository = blogPostRepository;
			this.likeRepository = likeRepository;
			this.signInManager = signInManager;
			this.userManager = userManager;
		}

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
			var liked = false;
            var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
			var blogDetailsViewModel = new BlogDetailsViewModel();

            if (blogPost != null)
            {
                var totalLikes = await likeRepository.GetTotalLikes(blogPost.Id);

				if (signInManager.IsSignedIn(User))
				{
					var likesForBlog = await likeRepository.GetLikesForBlog(blogPost.Id);

					var userId = userManager.GetUserId(User);

					if (userId != null)
					{
						var like = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
						liked = like != null;
					}
				}

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
					Liked = liked
				};

				return View(blogDetailsViewModel);
			}

            return View(blogDetailsViewModel);
        }
    }
}
