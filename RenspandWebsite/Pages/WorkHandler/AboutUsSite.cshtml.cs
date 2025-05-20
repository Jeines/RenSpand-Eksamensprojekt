using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service.AboutServices;

namespace RenspandWebsite.Pages.WorkHandler
{
    public class AboutUsSite : PageModel
    {
        private readonly AboutUsService _aboutService;

        public AboutUsSite(AboutUsService aboutService)
        {
            _aboutService = aboutService;
        }

        public AboutUs AboutContent { get; set; }
        public void OnGet()
        {
            AboutContent = _aboutService.GetAboutUs(1);
        }
    }
}

