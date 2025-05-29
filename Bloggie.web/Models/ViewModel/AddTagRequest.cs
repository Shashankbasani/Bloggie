using System.ComponentModel.DataAnnotations;

namespace Bloggie.web.Models.ViewModel
{
    public class AddTagRequest
    {
        [Required] 
        public string Name { get; set; }
        [Required]       
        public string DisplayName { get; set; }
    }
}
