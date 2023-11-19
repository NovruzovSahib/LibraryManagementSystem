using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModels
{
    public class RoleViewModel
    {
        public string? RoleId { get; set; }
        [Required(ErrorMessage ="This field must be filled"),StringLength(20)]
        public string? RoleName { get; set; }
    }
}
