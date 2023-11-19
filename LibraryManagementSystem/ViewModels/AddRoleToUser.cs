using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.ViewModels
{
    public class AddRoleToUser
    {
        public string? RoleId { get; set; }
        public string? UserId { get; set; }

        public List<IdentityUser>? UserList;

        public List<IdentityRole>? RolerList;
    }
}
