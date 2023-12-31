﻿using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "This field is required"), StringLength(20)]
        public string? Username { get; set; }
        [Required(ErrorMessage = "This field is required"), DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
