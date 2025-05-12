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

        //TODO: Update Name
        private readonly ProfileService _userService;

        private readonly PasswordHasher<string> passwordHasher;

        public CreateUserModel(ProfileService userService)
        {
            _userService = userService;
            passwordHasher = new PasswordHasher<string>();
        }

        public void OnGet()
        {
        }

        //TODO: Create Individual OnPost method
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

            _userService.AddProfile(newProfile);
            return RedirectToPage("/Index");
        }
    }
}
