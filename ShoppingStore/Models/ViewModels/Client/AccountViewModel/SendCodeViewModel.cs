using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApi.Models.ViewModels.Client.AccountViewModel
{
    public class SendCodeViewModel
    {
        [Required(ErrorMessage = "Phone Number is required")]
        [Display(Name = "PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
