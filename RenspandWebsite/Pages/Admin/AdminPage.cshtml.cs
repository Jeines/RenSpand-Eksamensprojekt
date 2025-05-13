using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service.ProfileServices;

namespace RenspandWebsite.Pages.Admin
{
    [Authorize(Roles = "admin")]
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
