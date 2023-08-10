namespace SwiftBlog.Web.Models.Domain
{
	public class Like
	{
		public Guid Id { get; set; }
		public Guid BlogPostId { get; set; }
		public Guid UserId { get; set; }
	}
}
