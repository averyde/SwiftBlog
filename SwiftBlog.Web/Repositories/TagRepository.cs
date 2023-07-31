using Microsoft.EntityFrameworkCore;
using SwiftBlog.Web.Data;
using SwiftBlog.Web.Models.Domain;

namespace SwiftBlog.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly SwiftBlogDbContext swiftBlogDbContext;

        public TagRepository(SwiftBlogDbContext swiftBlogDbContext)
        {
            this.swiftBlogDbContext = swiftBlogDbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await swiftBlogDbContext.Tags.AddAsync(tag);
            await swiftBlogDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await swiftBlogDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                swiftBlogDbContext.Tags.Remove(existingTag);
                await swiftBlogDbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await swiftBlogDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return swiftBlogDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await swiftBlogDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await swiftBlogDbContext.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }
    }
}
