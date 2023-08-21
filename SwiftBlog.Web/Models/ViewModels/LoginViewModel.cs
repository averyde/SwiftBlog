using System.ComponentModel.DataAnnotations;

namespace SwiftBlog.Web.Models.ViewModels
{
	public class LoginViewModel
	{
		[Required]
        public string Username { get; set; }
		[Required]
		[MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
		public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
