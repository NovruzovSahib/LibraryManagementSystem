using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "This field is required"), StringLength(20)]
        public string? Username { get; set; }
        [Required(ErrorMessage = "This field is required"), DataType(DataType.EmailAddress), StringLength(30)]
        public string? Email { get; set; }
        [Required(ErrorMessage = "This field is required"), DataType(DataType.Password), StringLength(10, ErrorMessage = "Password must be at least 10 characters long")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "This field is required"), DataType(DataType.Password), Compare("Password", ErrorMessage = "Password must be the same")]
        public string? ConfirmPassword { get; set; }
    }
}
