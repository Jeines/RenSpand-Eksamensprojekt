using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;
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

        public CreateModel(ProfileService userService)
        {
            _profileService = userService;
            passwordHasher = new PasswordHasher<string>();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Tjekker om brugeren allerede eksisterer
            if (_profileService.Profiles.Any(p => p.Username == UserName))
            {
                ModelState.AddModelError("UserName", "Username already exists.");
                return Page();
            }

            // Tjekker om Brugernavn er over 6 karakterer
            if (UserName.Length < 6)
            {
                ModelState.AddModelError("UserName", "Username must be at least 6 characters long.");
                return Page();
            }

            // Tjekker om passworder længere end 8 karakterer
            if (Password.Length < 8)
            {
                ModelState.AddModelError("Password", "Password must be at least 8 characters long.");
                return Page();
            }

            // Tjekker om Email er i valid format
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            if(!validateEmailRegex.IsMatch(Email))
            {
                ModelState.AddModelError("Email", "Invalid email format.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Profile newProfile = new Profile
            {
                Username = UserName,
                Email = Email,
                Role = RoleEnum.Private,
                Password = passwordHasher.HashPassword(null, Password)
            };

            _profileService.AddProfile(newProfile);
            
            return RedirectToPage("/Profiles/ProfileRedirection");
        }
    }
}