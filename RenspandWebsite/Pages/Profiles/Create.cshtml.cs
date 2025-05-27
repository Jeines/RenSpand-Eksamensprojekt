using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.EFDbContext;
using RenspandWebsite.Exceptions;
using RenspandWebsite.Models;
using RenspandWebsite.Service;
using RenspandWebsite.Service.ProfileServices;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace RenspandWebsite.Pages.Profiles
{
    //TODO Skal tjekkes igennem da den ikke virker til at oprette en profil med.
    public class CreateModel : PageModel
    {

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        private ProfileService _profileService;

        private PasswordHasher<string> passwordHasher;

        /// <summary>
        /// Konstruktør for CreateModel. Initialiserer ProfileService og PasswordHasher.
        /// </summary>
        /// <param name="userService">Service til håndtering af profiler</param>
        public CreateModel(ProfileService userService)
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
        /// Håndterer POST-anmodninger for at oprette en ny profil.
        /// Validerer input og opretter profil hvis alt er gyldigt.
        /// </summary>
        /// <returns>Redirect til profilside eller returnerer siden med fejl</returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                Profile newProfile = new Profile
                {
                    Username = UserName,
                    Email = Email,
                    Role = RoleEnum.Private,
                    Password = passwordHasher.HashPassword(null, Password)
                };

                _profileService.AddProfile(newProfile);
            }
            catch (InvalidUsernameException ex)
            {
                ModelState.AddModelError("UserName", ex.Message);
                return Page();

            }

            catch (InvalidPasswordException ex)
            {
                ModelState.AddModelError("Password", ex.Message);
                return Page();
            }

            catch (InvalidEmailException ex)
            {
                ModelState.AddModelError("Email", ex.Message);
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }
            return RedirectToPage("/Profiles/ProfileRedirection");
        }
    }
}