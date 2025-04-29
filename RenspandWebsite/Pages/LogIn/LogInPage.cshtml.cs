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
        public string UserName { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPost()
        {

            List<Profile> profiles = _profileService.Profiles;
            foreach (Profile profile in profiles)
            {
                if (UserName == profile.Username)
                {
                    var passwordHasher = new PasswordHasher<string>();

                    if (passwordHasher.VerifyHashedPassword(null, profile.Password, Password) == PasswordVerificationResult.Success)
                    {
                        CurrentUser = profile;

                        var claims = new List<Claim> { new Claim(ClaimTypes.Name, UserName) };

                        if (UserName == "admin") claims.Add(new Claim(ClaimTypes.Role, "admin"));

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        return RedirectToPage("/Item/GetAllItems");
                    }
                }
            }
            ErrorMessage = "Invalid attempt";
            return Page();
        }
    }
}
