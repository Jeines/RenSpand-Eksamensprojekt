using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service.ProfileServices;

namespace RenspandWebsite.Pages.Admin
{
    public class AdminPageModel : PageModel
    {
        private readonly ProfileService _profileService;

        public AdminPageModel(ProfileService profileService)
        {
            _profileService = profileService;
        }
        public void OnGetAsync()
        {
        }
    }
}
