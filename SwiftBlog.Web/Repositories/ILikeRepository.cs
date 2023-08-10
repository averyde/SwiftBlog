namespace SwiftBlog.Web.Repositories
{
	public interface ILikeRepository
	{
		Task<int> GetTotalLikes(Guid blogPost);
	}
}
