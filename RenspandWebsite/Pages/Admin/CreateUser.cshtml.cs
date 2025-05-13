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

        public CreateUserModel(ProfileService userService)
        {
            _profileService = userService;
            passwordHasher = new PasswordHasher<string>();
        }

        public void OnGet()
        {
        }

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
