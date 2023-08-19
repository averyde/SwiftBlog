using Microsoft.AspNetCore.Mvc;
using SwiftBlog.Web.Models.Domain;
using SwiftBlog.Web.Models.ViewModels;
using SwiftBlog.Web.Repositories;

namespace SwiftBlog.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogPostLikeController : ControllerBase
	{
		private readonly IBlogPostLikeRepository blogPostLikeRepository;

		public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
			this.blogPostLikeRepository = blogPostLikeRepository;
		}

        [HttpPost]
		[Route("Add")]
		public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
		{
			var model = new Like
			{
				BlogPostId = addLikeRequest.BlogPostId,
				UserId = addLikeRequest.UserId
			};

			await blogPostLikeRepository.AddLikeToBlog(model);

			return Ok();
		}

		[HttpGet]
		[Route("{blogPostId:Guid}/totalLikes")]
		public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
		{
			var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogPostId);

			return Ok(totalLikes);
		}
	}
}
