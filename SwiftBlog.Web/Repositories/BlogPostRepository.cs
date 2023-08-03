using Microsoft.EntityFrameworkCore;
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

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogPost = await swiftBlogDbContext.BlogPosts.FindAsync(id);

            if (existingBlogPost != null)
            {
                swiftBlogDbContext.BlogPosts.Remove(existingBlogPost);
                await swiftBlogDbContext.SaveChangesAsync();
                return existingBlogPost;
            }

            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await swiftBlogDbContext.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await swiftBlogDbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await swiftBlogDbContext.BlogPosts.FindAsync(blogPost.Id);

            if (existingBlogPost != null)
            {
                existingBlogPost.Heading = blogPost.Heading;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlogPost.UrlHandle = blogPost.UrlHandle;
                existingBlogPost.PublishedDate = blogPost.PublishedDate;
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.Visible = blogPost.Visible;
                existingBlogPost.Tags = blogPost.Tags;

                await swiftBlogDbContext.SaveChangesAsync();

                return existingBlogPost;
            }

            return null;
        }
    }
}
