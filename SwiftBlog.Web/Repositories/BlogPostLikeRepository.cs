using Microsoft.EntityFrameworkCore;
using SwiftBlog.Web.Data;
using SwiftBlog.Web.Models.Domain;

namespace SwiftBlog.Web.Repositories
{
	public class BlogPostLikeRepository : IBlogPostLikeRepository
	{
		private readonly SwiftBlogDbContext swiftBlogDbContext;

		public BlogPostLikeRepository(SwiftBlogDbContext swiftBlogDbContext)
		{
			this.swiftBlogDbContext = swiftBlogDbContext;
		}

		public async Task<Like> AddLikeToBlog(Like like)
		{
			await swiftBlogDbContext.Likes.AddAsync(like);
			await swiftBlogDbContext.SaveChangesAsync();
			return like;
		}

		public async Task<IEnumerable<Like>> GetLikesForBlog(Guid blogPostId)
		{
			return await swiftBlogDbContext.Likes.Where(x => x.BlogPostId == blogPostId).ToListAsync();
		}

		public async Task<int> GetTotalLikes(Guid blogPostId)
		{
			return await swiftBlogDbContext.Likes.CountAsync(x => x.BlogPostId == blogPostId);
		}
	}
}
