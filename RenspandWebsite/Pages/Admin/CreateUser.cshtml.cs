using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service.ProfileServices;
using System.ComponentModel.DataAnnotations;

namespace RenspandWebsite.Pages.Admin
{
    [Authorize(Roles = "admin")]
    public class CreateUserModel : PageModel
    {
        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        private readonly ProfileService _profileService;

        private readonly PasswordHasher<string> passwordHasher;

        /// <summary>
        /// Konstruktør der initialiserer ProfileService og PasswordHasher.
        /// </summary>
        /// <param name="userService">Service til håndtering af profiler</param>
        public CreateUserModel(ProfileService userService)
        {
            _profileService = userService;
            passwordHasher = new PasswordHasher<string>();
        }

        /// <summary>
        /// Håndterer GET-anmodninger til siden.
        /// </summary>
        public void OnGet()
        {
        }

        /// <summary>
        /// Håndterer POST-anmodninger for at oprette en ny bruger.
        /// Validerer model, opretter profil og tilføjer den via ProfileService.
        /// </summary>
        /// <returns>Redirect til forsiden hvis succes, ellers vises siden igen.</returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Profile newProfile = new()
            {
                Username = UserName,
                Email = Email,
                Password = passwordHasher.HashPassword(null, Password)
            };

            _profileService.AddProfile(newProfile);
            return RedirectToPage("/Index");
        }
    }
}
