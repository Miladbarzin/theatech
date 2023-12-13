using DataLayer.DBModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.ViewModels
{
    public class UsersViewModel
    {
        public List<ShowUsers> Users { get; set; }
        public int PageCount { get; set; }
    }

    public class ShowUsers
    {
        public string UserId { get; set; }
        public string? Phonenumber { get; set; }
        public string Email { get; set; }
        public string? avatar { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<string> RoleID { get; set; }
        public List<string> RoleTile { get; set; }
        public bool IsLock { get; set; }
        public bool PhoneConfirmation { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime RegisterDate { get; set; }
    }
    public class CreateUser
    {
        [Display(Name = "Phonenumber")]
        [MaxLength(11, ErrorMessage = "Phone number is invalid")]
        public string? Phonenumber { get; set; }
        [Display(Name = "Avatar")]
        public IFormFile? avatar { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum character is 50")]
        [Display(Name = "FirstName")]
        public string? FirstName { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum character is 50")]
        [Display(Name = "LastName")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(50, ErrorMessage = "Maximum character is 50")]
        [MinLength(6, ErrorMessage = "Maximum character is 6")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "Password must has character and number")]
        public string Password { get; set; }
        public List<string> RoleID { get; set; }
        [Display(Name = "IsVerified")]
        public bool IsVerified { get; set; }
    }

    public class EditUser
    {
        public string UserId { get; set; }
        [Display(Name = "Avatar")]
        public IFormFile? avatar { get; set; }
        public string? avatarpath { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum character is 50")]
        [Display(Name = "FirstName")]
        public string? FirstName { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum character is 50")]
        [Display(Name = "LastName")]
        public string? LastName { get; set; }
        public List<string> RoleTitle { get; set; }
        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }
        [Display(Name = "IsVerified")]
        public bool IsVerified { get; set; }
        [Display(Name = "IsDeleted")]
        public bool IsDeleted { get; set; }
    }



    public class Userpanel
    {
        public IdentityUser user { get; set; }
    }
    public class EditUserPanel
    {
        public string UserId { get; set; }
        [Display(Name = "Phonenumber")]
        [MaxLength(11, ErrorMessage = "Phone number is invalid")]
        public string? Phonenumber { get; set; }
        [Display(Name = "Avatar")]
        public IFormFile? avatar { get; set; }
        public string? avatarpath { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum character is 50")]
        [Display(Name = "نام")]
        public string? FirstName { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum character is 50")]
        [Display(Name = "LastName")]
        public string? LastName { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum character is 50")]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword")]
        public string? OldPassword { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum character is 50")]
        [MinLength(5, ErrorMessage = "Minimum character is 5")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum character is 50")]
        [MinLength(5, ErrorMessage = "Minimum character is 5")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "RePassword not matched")]
        [Display(Name = "RePassword")]
        public string? RePassword { get; set; }
    }

    public class EditUserPanelStatus
    {
        public bool Status { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class UserChart
    {
        public int Date { get; set; }
        public int Value { get; set; }
    }
}
