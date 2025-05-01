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
        public static Profile CurrentUser { get; set; } = null;
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


        public async Task<IActionResult> OnPost()
        {
            List<Profile> profiles = _profileService.Profiles;
            foreach (Profile profile in profiles)
            {
                if (Username == profile.Username)
                {
                    var passwordHasher = new PasswordHasher<string>();
                    if (passwordHasher.VerifyHashedPassword(null, profile.Password, Password) == PasswordVerificationResult.Success)
                    {
                        CurrentUser = profile;

                        var claims = new List<Claim> { new Claim(ClaimTypes.Name, Username) };
                        string redirectPage = "/Index";

                        Console.WriteLine(profile.Role);
                        switch (profile.Role)
                        {
                            case RoleEnum.Admin:
                                claims.Add(new Claim(ClaimTypes.Role, "admin"));
                                redirectPage = "/Admin/AdminPage";
                                break;
                            case RoleEnum.Employee:
                                claims.Add(new Claim(ClaimTypes.Role, "employee"));
                                redirectPage = "/Employee/EmployeePage";
                                break;
                            case RoleEnum.Business:
                                claims.Add(new Claim(ClaimTypes.Role, "business"));
                                redirectPage = "/Business/BusinessPage";
                                break;
                            case RoleEnum.Private:
                                claims.Add(new Claim(ClaimTypes.Role, "private"));
                                redirectPage = "/Private/PrivatePage";
                                break;
                            default:
                                claims.Add(new Claim(ClaimTypes.Role, "guest"));
                                break;
                        }
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