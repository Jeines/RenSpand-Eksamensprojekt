using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;

namespace RenspandWebsite.Pages.Profiles
{
    public class ProfileSiteModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [BindProperty]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [BindProperty]
        [Display(Name = "Telefon Nummer: +45")]
        public string PhoneNumber { get; set; }

        [BindProperty]
        [Display(Name = "Navn")]
        public string Name { get; set; }
        //[Display(Name = "Adresse")]
        //public string Address { get; set; }

        [BindProperty]
        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [BindProperty]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [BindProperty]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        private ProfileService _profileService;

        public ProfileSiteModel(ProfileService profileService)
        {
            _profileService = profileService;
        }
        public void OnGet()
        {
            //Tjekker om brugeren er logget ind
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToPage("/LogIn/LogInPage");
            }

            // Tjekker for ugyldigt user ID
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Henter id'et fra brugeren der er logget ind
            var user = _profileService.GetUserData(int.Parse(userIdClaim));

            // sætter felter på siden til brugerens data
            Username = user.Username;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Name = user.Name;
        }

        /// <summary>
        /// Opdaterer brugerens profiloplysninger.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostSaveChanges()
        {
            // Fjerner validering for felter, der ikke er relevante for denne handling
            ModelState.Remove(nameof(Username));
            ModelState.Remove(nameof(Email));
            ModelState.Remove(nameof(CurrentPassword));
            ModelState.Remove(nameof(NewPassword));
            ModelState.Remove(nameof(ConfirmPassword));

            // Validerer modelen
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Opretter et nyt Profile-objekt med de opdaterede oplysninger
            var updatedProfile = new Profile
            {
                Username = Username,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Name = Name
            };

            //Henter userens ID fra claims
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Opdaterer brugerens data i databasen
            _profileService.UpdateUserData(userId, updatedProfile);

            return RedirectToPage();
        }


        /// <summary>
        /// Opdaterer brugerens adgangskode.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostChangePassword()
        {
            // Fjerner validering for felter, der ikke er relevante for denne handling
            ModelState.Remove(nameof(Name));
            ModelState.Remove(nameof(PhoneNumber));
            ModelState.Remove(nameof(Email));
            ModelState.Remove(nameof(Username));

            // Validerer modelen
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Henter userens ID fra claims
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));


            // Tjekker om den nuværende adgangskode er korrekt
            bool validPassword = _profileService.ValidatePassword(userId, CurrentPassword);

            // Hvis den nuværende adgangskode ikke er korrekt, tilføj en fejl til ModelState
            if (!validPassword)
            {
                ModelState.AddModelError(nameof(CurrentPassword), "Fokert adgangskode");
                return Page();
            }

            // Opdaterer adgangskoden med den nye adgangskode
            _profileService.UpdatePassWord(userId, NewPassword);
            return RedirectToPage();
        }

        /// <summary>
        /// Sletter brugerens profil og logger dem ud.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostDeleteProfileAsync()
        {

            //Henter userens ID fra claims
            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Fjerner profilen fra databasen
            _profileService.RemoveProfile(userId);

            // Logger brugeren ud
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/index");
        }
    }
}