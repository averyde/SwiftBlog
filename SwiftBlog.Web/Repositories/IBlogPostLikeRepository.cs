using SwiftBlog.Web.Models.Domain;

namespace SwiftBlog.Web.Repositories
{
	public interface IBlogPostLikeRepository
	{
		Task<int> GetTotalLikes(Guid blogPostId);

		Task<IEnumerable<Like>> GetLikesForBlog(Guid blogPostId);

		Task<Like> AddLikeToBlog(Like like);
	}
}
