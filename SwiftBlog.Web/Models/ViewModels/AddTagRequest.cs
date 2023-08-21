using System.ComponentModel.DataAnnotations;

namespace SwiftBlog.Web.Models.ViewModels
{
    public class AddTagRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string DisplayName { get; set; }
        public string Color { get; set; }
    }
}
