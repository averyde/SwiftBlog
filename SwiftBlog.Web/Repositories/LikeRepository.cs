using Microsoft.EntityFrameworkCore;
using SwiftBlog.Web.Data;

namespace SwiftBlog.Web.Repositories
{
	public class LikeRepository : ILikeRepository
	{
		private readonly SwiftBlogDbContext swiftBlogDbContext;

		public LikeRepository(SwiftBlogDbContext swiftBlogDbContext)
		{
			this.swiftBlogDbContext = swiftBlogDbContext;
		}

		public async Task<int> GetTotalLikes(Guid blogPostId)
		{
			return await swiftBlogDbContext.Likes.CountAsync(x => x.BlogPostId == blogPostId);
		}
	}
}
