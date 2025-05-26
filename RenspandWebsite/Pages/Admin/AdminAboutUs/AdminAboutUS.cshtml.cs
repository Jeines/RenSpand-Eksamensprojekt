using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Models;
using RenspandWebsite.Service.AboutServices;

namespace RenspandWebsite.Pages.Admin.AdminAboutUs
{
   [Authorize(Roles = "admin")]
    public class AdminAboutUs : PageModel
    {
        private readonly AboutUsService _aboutService;

        public AdminAboutUs(AboutUsService aboutUsService)
        {
            _aboutService = aboutUsService;
        }

        [BindProperty]
        public AboutUs AboutUs { get; set; } = new AboutUs();

        public void OnGet()
        {
            AboutUs = _aboutService.GetAboutUs(1);
        }
        /// <summary>  
        /// Håndterer POST-anmodningen for at opdatere "About Us"-informationen.  
        /// </summary>  
        /// <returns>  
        /// Returnerer en side, hvis modeltilstanden er ugyldig, ellers omdirigeres til siden "/workHandler/AboutUsSite".  
        /// </returns>  
        public IActionResult OnPost()
        {
            // Kontrollerer, om modeltilstanden er gyldig.  
            if (!ModelState.IsValid)
            {
                // Returnerer den aktuelle side, hvis der er valideringsfejl.  
                return Page();
            }

            // Opdaterer "About Us"-informationen med ID 1 ved hjælp af AboutUsService.
            _aboutService.UpdateAboutUS(1, AboutUs);

            // Omdirigerer brugeren til siden "/workHandler/AboutUsSite".  
            return RedirectToPage("/workHandler/AboutUsSite");
        }
    }
}