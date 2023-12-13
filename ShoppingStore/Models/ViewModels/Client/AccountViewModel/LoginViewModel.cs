using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApi.Models.ViewModels.AccountViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Address is required!")]
        [Display(Name = " Email Address ")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }


        [Required(ErrorMessage = "Password is required!")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


       // public string? ReturnUrl { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
