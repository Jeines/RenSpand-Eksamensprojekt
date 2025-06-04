using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;
using RenspandWebsite.Service.ProfileServices;
using RenspandWebsite.Models;
using System.Threading.Tasks;

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
        public async void OnGetAsync()
        {
            //Tjekker om brugeren er logget ind
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToPage("/LogIn/LogInPage");
            }

            // Tjekker for ugyldigt user ID
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Henter id'et fra brugeren der er logget ind
            var user = await _profileService.GetUserDataAsync(int.Parse(userIdClaim));

            // sætter felter på siden til brugerens data
            Username = user.Username;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Name = user.Name;
        }

        /// <summary>
        /// Opdaterer brugerens profiloplysninger.
        /// </summary>
        /// <returns>En IActionResult, der enten genindlæser siden eller viser valideringsfejl.</returns>
        public IActionResult OnPostSaveChanges()
        {
            // Fjerner validering for felter, der ikke er relevante for denne handling
            ModelState.Remove(nameof(Username));
            ModelState.Remove(nameof(Email));
            ModelState.Remove(nameof(CurrentPassword));
            ModelState.Remove(nameof(NewPassword));
            ModelState.Remove(nameof(ConfirmPassword));

            // Validerer modellen
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

            // Henter brugerens ID fra claims
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Opdaterer brugerens data i databasen
            _profileService.UpdateUserData(userId, updatedProfile);

            return RedirectToPage();
        }


        /// <summary>
        /// Opdaterer brugerens adgangskode.
        /// </summary>
        /// <returns>En IActionResult, der enten genindlæser siden eller viser valideringsfejl.</returns>
        public async Task<IActionResult> OnPostChangePassword()
        {
            // Fjerner validering for felter, der ikke er relevante for denne handling
            ModelState.Remove(nameof(Name));
            ModelState.Remove(nameof(PhoneNumber));
            ModelState.Remove(nameof(Email));
            ModelState.Remove(nameof(Username));

            // Validerer modellen
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Henter brugerens ID fra claims
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Tjekker om den nuværende adgangskode er korrekt
            bool validPassword = await _profileService.ValidatePassword(userId, CurrentPassword);

            // Hvis den nuværende adgangskode ikke er korrekt, tilføj en fejl til ModelState
            if (!validPassword)
            {
                ModelState.AddModelError(nameof(CurrentPassword), "Forkert adgangskode");
                return Page();
            }

            // Opdaterer adgangskoden med den nye adgangskode
            _profileService.UpdatePassWord(userId, NewPassword);
            return RedirectToPage();
        }

        /// <summary>
        /// Sletter brugerens profil og logger brugeren ud.
        /// </summary>
        /// <returns>En Task<IActionResult>, der omdirigerer til forsiden efter sletning og logud.</returns>
        public async Task<IActionResult> OnPostDeleteProfileAsync()
        {

            // Henter brugerens ID fra claims
            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Fjerner profilen fra databasen
            _profileService.RemoveProfile(userId);

            // Logger brugeren ud
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/index");
        }
    }
}