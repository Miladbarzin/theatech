using DataLayer.DBModels;
using DataLayer.Repository;
using DataLayer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models.ViewModels.AccountViewModel;
using WebApi.Models.ViewModels.Client.AccountViewModel;

namespace WebApi.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly UserManager<AspNetUser> _userManager;
        IUserStore<AspNetUser> _userStore;
        IConfiguration configuration;
        ILogger<AuthController> _logger;
        IUserPhoneNumberStore<AspNetUser> _phoneNumberStore;
        IJWTAuthManager _jWTAuthManager;

        public AuthController(SignInManager<AspNetUser> signInManager
           , UserManager<AspNetUser> userManager
           , IUserStore<AspNetUser> userStore
           , ILogger<AuthController> logger
           , IConfiguration configuration
           , IJWTAuthManager jWTAuthManager)
        {
            this.configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _logger = logger;
            _phoneNumberStore = GetPhoneNumberStore();
            _jWTAuthManager = jWTAuthManager;
        }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public TimeSpan ExpireDate { get; set; } = TimeSpan.FromMinutes(3);
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel register)
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var userExists = await _userManager.FindByNameAsync(register.EmailAddress);
            if (userExists != null)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "400", Message = "EmailAddress is existed!" });
          
            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, register.EmailAddress, CancellationToken.None);

            await _phoneNumberStore.SetPhoneNumberAsync(user, register.EmailAddress, CancellationToken.None);
            user.Firstname = "";
            user.Lastname = "";
            user.CreateDateTime = DateTime.Now;

            // we can add confirmation phone number and email address here
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created!");


                await _signInManager.SignInAsync(user, isPersistent: true);

                return Ok(new Response { Status = "200", Message = "User created!" });
            }
            else
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "400", Message = result.Errors.First().Description });

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var user = await _userManager.FindByNameAsync(model.EmailAddress);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "404", Message = "User not exist!" });
            }
            var passwordCorrectResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (passwordCorrectResult.Succeeded)
            {

                await _signInManager.SignInAsync(user, isPersistent: true);

                var jwtToken = await _jWTAuthManager.GenerateJWT(user);

                await _signInManager.SignInAsync(user, isPersistent: true);

                return Ok(new Response { Status = "200", Message = "User Logged in successfully!", Data = jwtToken });

            }
            else if (passwordCorrectResult.IsLockedOut)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "403", Message = "User locked! Try later!" });

            }
            else if (!passwordCorrectResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new Response { Status = "401", Message = "Password is incorrect!" });
            }
            else
            {
                await _signInManager.SignOutAsync();
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = _userManager.GetUserId(User);
            await _jWTAuthManager.RemoveToken(userId);
            return Ok(new Response { Status = "200", Message = "Logged out successfully!" });
        }

        private AspNetUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AspNetUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AspNetUser)}'. " +
                    $"Ensure that '{nameof(AspNetUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserPhoneNumberStore<AspNetUser> GetPhoneNumberStore()
        {
            return (IUserPhoneNumberStore<AspNetUser>)_userStore;
        }
    }
}
