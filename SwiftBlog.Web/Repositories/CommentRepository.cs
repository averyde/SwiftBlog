using Microsoft.EntityFrameworkCore;
using SwiftBlog.Web.Data;
using SwiftBlog.Web.Models.Domain;

namespace SwiftBlog.Web.Repositories
{
	public class CommentRepository : ICommentRepository
	{
		private readonly SwiftBlogDbContext swiftBlogDbContext;

		public CommentRepository(SwiftBlogDbContext swiftBlogDbContext)
        {
			this.swiftBlogDbContext = swiftBlogDbContext;
		}

        public async Task<Comment> AddAsync(Comment comment)
		{
			await swiftBlogDbContext.Comments.AddAsync(comment);
			await swiftBlogDbContext.SaveChangesAsync();
			return comment;
		}

		public async Task<IEnumerable<Comment>> GetAllAsync(Guid blogPostId)
		{
			return await swiftBlogDbContext.Comments.Where(x => x.BlogPostId == blogPostId).ToListAsync();
		}
	}
}
