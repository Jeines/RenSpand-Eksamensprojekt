using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;
using RenspandWebsite.Service;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

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

        private ProfileService _userService;

        private PasswordHasher<string> passwordHasher;

        public CreateModel(ProfileService userService)
        {
            _userService = userService;
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

            Profile newProfile = new Profile
            {
                Username = UserName,
                Email = Email,
                Role = RoleEnum.Private,
                Password = passwordHasher.HashPassword(null, Password)
            };


            _userService.AddProfile(newProfile);
            
            return RedirectToPage("/Profiles/ProfileRedirection");
        }

        

    }
}