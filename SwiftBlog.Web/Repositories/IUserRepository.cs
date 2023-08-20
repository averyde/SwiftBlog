using Microsoft.AspNetCore.Identity;

namespace SwiftBlog.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
