using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace RenspandWebsite.Pages.LogIn
{
    public class LogInPageModel : PageModel
    {
        private ProfileService _profileService;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

        public LogInPageModel(ProfileService profileService)
        {
            _profileService = profileService;
        }

        public void OnGet()
        {
        }

        // This method is called when the user submits the login form
        public async Task<IActionResult> OnPost()
        {
            List<Profile> profiles = _profileService.Profiles;
            foreach (Profile profile in profiles)
            {
                // Check if the username matches
                if (Username == profile.Username)
                {
                    // Verify the password using PasswordHasher
                    var passwordHasher = new PasswordHasher<string>();
                    if (passwordHasher.VerifyHashedPassword(null, profile.Password, Password) == PasswordVerificationResult.Success)
                    {

                        var claims = new List<Claim> { new Claim(ClaimTypes.Name, Username) };
                        string redirectPage = "/Index";

                        // Set the role claim based on the user's role
                        switch (profile.Role)
                        {
                            case RoleEnum.Admin:
                                claims.Add(new Claim(ClaimTypes.Role, "admin"));
                                claims.Add(new Claim(ClaimTypes.Name, profile.Username));
                                redirectPage = "/Admin/AdminPage";
                                break;
                            case RoleEnum.Employee:
                                claims.Add(new Claim(ClaimTypes.Role, "employee"));
                                claims.Add(new Claim(ClaimTypes.Name, profile.Username));
                                redirectPage = "/Employee/EmployeePage";
                                break;
                            case RoleEnum.Business:
                                claims.Add(new Claim(ClaimTypes.Role, "business"));
                                claims.Add(new Claim(ClaimTypes.Name, profile.Username));
                                redirectPage = "/Business/BusinessPage";
                                break;
                            case RoleEnum.Private:
                                claims.Add(new Claim(ClaimTypes.Role, "private"));
                                claims.Add(new Claim(ClaimTypes.Name, profile.Username));
                                redirectPage = "/Private/PrivatePage";
                                break;
                            default:
                                claims.Add(new Claim(ClaimTypes.Role, "guest"));
                                break;
                        }
                        // Create the claims identity and sign in the user
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        return RedirectToPage(redirectPage);
                    }
                }
            }
            ErrorMessage = "Invalid attempt";
            return Page();
        }
    }
}