﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApi.Models.ViewModels.Client.AccountViewModel
{
    public class ResetPasswordViewModel
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
