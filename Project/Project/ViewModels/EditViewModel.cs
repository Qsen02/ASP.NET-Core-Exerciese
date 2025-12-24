using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class EditViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Title must be at least 3 symbols long!")]
        public string Title { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Content must be at least 10 symbols long!")]
        public string Content { get; set; }
    }
}
