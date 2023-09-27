using Microsoft.AspNetCore.Identity;

namespace SwiftBlog.Web.Models.Domain
{
	public class User : IdentityUser
	{
		public string About { get; set; }
		public string ProfileUrl { get; set; }
		public string Role { get; set; }
		public bool IsDarkMode { get; set; }
	}
}
