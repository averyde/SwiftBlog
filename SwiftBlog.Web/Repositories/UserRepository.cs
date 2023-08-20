using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SwiftBlog.Web.Data;

namespace SwiftBlog.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext authDbContext;

        public UserRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            return await authDbContext.Users.Where(x => x.Email != "superadmin@swiftblog.com").ToListAsync();
        }
    }
}
