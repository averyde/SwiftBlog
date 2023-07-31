using SwiftBlog.Web.Data;
using SwiftBlog.Web.Models.Domain;

namespace SwiftBlog.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly SwiftBlogDbContext swiftBlogDbContext;

        public BlogPostRepository(SwiftBlogDbContext swiftBlogDbContext)
        {
            this.swiftBlogDbContext = swiftBlogDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await swiftBlogDbContext.AddAsync(blogPost);
            await swiftBlogDbContext.SaveChangesAsync();
            return blogPost;
        }

        public Task<BlogPost?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
