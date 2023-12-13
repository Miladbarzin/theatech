using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApi.Models.ViewModels.AccountViewModel
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Email Address is required!")]
        [Display(Name = " Email Address ")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(100, ErrorMessage = "Password must minimum {1} and maximum {2}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Repassword")]
        [Compare("Password", ErrorMessage = "Password and rePassword not match!")]
        public string RePassword { get; set; }
      
       
    }
}
