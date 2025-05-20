using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service.AboutServices;

namespace RenspandWebsite.Pages.WorkHandler
{
    public class AboutUsSite : PageModel
    {
        private readonly AboutUsService _aboutService;

        // Konstruktør der modtager en AboutUsService via dependency injection.
        public AboutUsSite(AboutUsService aboutService)
        {
            _aboutService = aboutService;
        }

        // Egenskab der indeholder "Om os"-indholdet, som vises på siden.
        public AboutUs AboutContent { get; set; }

        // OnGet-metoden henter "Om os"-indholdet med id 1, når siden indlæses.
        public void OnGet()
        {
            AboutContent = _aboutService.GetAboutUs(1);
        }
    }
}

