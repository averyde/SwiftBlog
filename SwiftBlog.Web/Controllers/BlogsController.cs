using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SwiftBlog.Web.Models.Domain;
using SwiftBlog.Web.Models.ViewModels;
using SwiftBlog.Web.Repositories;

namespace SwiftBlog.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
		private readonly ILikeRepository likeRepository;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly UserManager<IdentityUser> userManager;
		private readonly ICommentRepository commentRepository;

		public BlogsController(IBlogPostRepository blogPostRepository, 
			ILikeRepository likeRepository, 
			SignInManager<IdentityUser> signInManager, 
			UserManager<IdentityUser> userManager,
			ICommentRepository commentRepository)
        {
            this.blogPostRepository = blogPostRepository;
			this.likeRepository = likeRepository;
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.commentRepository = commentRepository;
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

				var comments = await commentRepository.GetAllAsync(blogPost.Id);

				var commentsForView = new List<BlogPostComment>();

				foreach (var comment in comments) 
				{
					commentsForView.Add(new BlogPostComment
					{
						Description = comment.Description,
						DateAdded = comment.DateAdded,
						Username = (await userManager.FindByIdAsync(comment.UserId.ToString())).UserName
					});
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
					Liked = liked,
					Comments = commentsForView
				};

				return View(blogDetailsViewModel);
			}

            return View(blogDetailsViewModel);
        }

		[HttpPost]
		public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
		{
			if (signInManager.IsSignedIn(User))
			{
				var domainModel = new Comment
				{
					BlogPostId = blogDetailsViewModel.Id,
					Description = blogDetailsViewModel.CommentDescription,
					UserId = Guid.Parse(userManager.GetUserId(User)),
					DateAdded = DateTime.Now
				};

				await commentRepository.AddAsync(domainModel);
				return RedirectToAction("Index", "Blogs", new { urlHandle = blogDetailsViewModel.UrlHandle });
			}

			return Forbid();
		}
    }
}
