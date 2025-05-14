using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service.ProfileServices;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace RenspandWebsite.Pages.LogIn
{
    public class LogInPageModel : PageModel
    {
        private readonly ProfileService _profileService;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public LogInPageModel(ProfileService profileService)
        {
            _profileService = profileService;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            // finder profilen ud fra username og tjekker om den eksisterer
            var profile = _profileService.Profiles.FirstOrDefault(p => p.Username == Username);
            if (profile == null)
            {
                ErrorMessage = "Invalid attempt";
                return Page();
            }

            // Opretter en PasswordHasher
            var passwordHasher = new PasswordHasher<string>();

            // Verificerer passwordet
            var verificationResult = passwordHasher.VerifyHashedPassword(null, profile.Password, Password);

            // hvis passwordet ikke er korrekt, returner fejl
            if (verificationResult != PasswordVerificationResult.Success)
            {
                ErrorMessage = "Invalid attempt";
                return Page();
            }

            // Tilføjer claims til claimsIdentity
            var claims = BuildClaims(profile);

            // Opretter en ClaimsIdentity og logger brugeren ind
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            // Redirecter til den relevante side baseret på brugerens rolle
            return RedirectToPage(GetRedirectPage(profile.Role));
        }

        /// <summary>
        /// Laver claims ud fra profilen der er logget ind
        /// </summary>
        /// <param name="profile">Profilen der er logget ind</param>
        /// <returns></returns>
        private List<Claim> BuildClaims(Profile profile)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Name, profile.Username),
                new Claim(ClaimTypes.Role, profile.Role.ToString().ToLower()),
                new Claim(ClaimTypes.NameIdentifier, profile.Id.ToString())
            };
        }

        /// <summary>
        /// Giver den relevante side baseret på brugerens rolle
        /// </summary>
        /// <param name="role">Rolle som briger har i databasen</param>
        /// <returns></returns>
        private string GetRedirectPage(RoleEnum role)
        {
            return role switch
            {
                RoleEnum.Admin => "/Admin/AdminPage",
                RoleEnum.Employee => "/Employee/EmployeePage",
                RoleEnum.Business => "/Business/BusinessPage",
                RoleEnum.Private => "/Index",
                _ => "/Index"
            };
        }
    }
}