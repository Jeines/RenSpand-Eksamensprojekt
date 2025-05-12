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

    //TODO: Kig på alt logikken i OnGet og OnPost metoderne, da de ikke virker til at opdatere profilen.
    //TODO: Vær sikker på at de rette elementer bliver rettet og hvad der rent faktisk skal opdateres. 
    //(Password, PhoneNumber, Address) Alt anden føles som unødvendigt og uden for normer brugt i dag på andre websites.
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
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Console.WriteLine("Logged in user ID: " + userId);
                Console.WriteLine(_profileService.GetUserData(Int32.Parse(userId)));

                Email = _profileService.GetUserData(Int32.Parse(userId)).Email;
                Username = _profileService.GetUserData(Int32.Parse(userId)).Username;
                PhoneNumber = _profileService.GetUserData(Int32.Parse(userId)).PhoneNumber;
                Name = _profileService.GetUserData(Int32.Parse(userId)).Name;
            }
            else
            {
                Console.WriteLine("User is not authenticated.");
            }
        }


        public IActionResult OnPostSaveChanges()
        {
            Console.WriteLine("OnPostSaveChanges");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _profileService.UpdateUserData(Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), new Profile
            {
                Username = Username,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Name = Name
            });

            // Logic to save changes to the user's profile
            // Example: Update the database with the new data
            TempData["Message"] = "Profile changes saved successfully!";
            return RedirectToPage();
        }

        public IActionResult OnPostChangePassword()
        {
            Console.WriteLine("onpostChangePassword");
            if (!ModelState.IsValid)
            {
                foreach (var modelStateEntry in ModelState)
                {
                    var key = modelStateEntry.Key;
                    foreach (var error in modelStateEntry.Value.Errors)
                    {
                        Console.WriteLine($"ModelState error for '{key}': {error.ErrorMessage}");
                    }
                }
                Console.WriteLine("Du er Her");
                return Page();
            }

            Console.WriteLine(_profileService.GetPassword(Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
, CurrentPassword));
            Console.WriteLine("Update Passwerd");
            _profileService.UpdatePassWord(Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
, NewPassword);



            // Logic to change the user's password
            // Example: Validate current password and update to new password
            TempData["Message"] = "Password changed successfully!";
            return RedirectToPage();
        }
    }
}