using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Admin
{
    public class AdminPageModel : PageModel
    {
        private readonly ProfileService _profileService;

        public AdminPageModel(ProfileService profileService)
        {
            _profileService = profileService;
        }

        //TODO: Remove onget async and post async methods when done testing
        public async Task OnGetAsync()
        {
            var profiles = _profileService.Profiles; 
        }
    }
}
