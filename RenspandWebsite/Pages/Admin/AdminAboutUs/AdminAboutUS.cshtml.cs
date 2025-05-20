using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                
                return Page();
            }
            _aboutService.UpdateAboutUS(1,AboutUs);
            
            return RedirectToPage("/workHandler/AboutUsSite");
        }
    }
}