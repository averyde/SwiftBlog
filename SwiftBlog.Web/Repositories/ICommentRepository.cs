using SwiftBlog.Web.Models.Domain;

namespace SwiftBlog.Web.Repositories
{
	public interface ICommentRepository
	{
		Task<Comment> AddAsync(Comment comment);

		Task<IEnumerable<Comment>> GetAllAsync(Guid blogPostId);
	}
}
