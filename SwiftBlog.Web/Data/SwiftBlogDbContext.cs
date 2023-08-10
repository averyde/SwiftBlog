using Microsoft.EntityFrameworkCore;
using SwiftBlog.Web.Models.Domain;

namespace SwiftBlog.Web.Data
{
    public class SwiftBlogDbContext : DbContext
    {
        public SwiftBlogDbContext(DbContextOptions<SwiftBlogDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
		public DbSet<Like> Likes { get; set; }
	}
}
