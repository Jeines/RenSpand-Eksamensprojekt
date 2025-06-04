using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service.ProfileServices;

namespace RenspandWebsite.Pages.Admin
{
    /// <summary>
    /// Siden for administratorer. Kræver at brugeren har rollen 'admin'.
    /// </summary>
    [Authorize(Roles = "admin")]
    public class AdminPageModel : PageModel
    {
        private readonly ProfileService _profileService;

        /// <summary>
        /// Initialiserer en ny instans af <see cref="AdminPageModel"/> med den angivne <see cref="ProfileService"/>.
        /// </summary>
        /// <param name="profileService">Service til håndtering af profiler.</param>
        public AdminPageModel(ProfileService profileService)
        {
            _profileService = profileService;
        }

        /// <summary>
        /// Henter alle profiler, når siden indlæses.
        /// </summary>
        public void OnGet()
        {
        }
    }
}
