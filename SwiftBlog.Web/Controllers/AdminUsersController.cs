using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwiftBlog.Web.Models.ViewModels;
using SwiftBlog.Web.Repositories;

namespace SwiftBlog.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository userRepository;

        public AdminUsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IActionResult> List()
        {
            var users = await userRepository.GetAll();

            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();

            foreach (var user in users)
            {
                usersViewModel.Users.Add(new User
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    Email = user.Email
                });
            }

            return View(usersViewModel); 
        }
    }
}
