using Microsoft.AspNetCore.Authorization;
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
            var profiles = _profileService.Profiles; 
            //await _profileService.AddTestAddressAsync();

            var profiles = _profileService.Profiles; 
            foreach (var profile in profiles)
            {
                Console.WriteLine($"Id: {profile.Id}, Username: {profile.Username}, Email: {profile.Email}");
            }


            //Console.WriteLine("\nFrom Database:");
            //var dbProfiles = await _profileService.GetProfilesFromDb();
            //foreach (var profile in dbProfiles)
            //{
            //    Console.WriteLine(profile.ToString());
            //}
            //await _profileService.AddTestAddressAsync();

            var profiles = _profileService.Profiles; 
            foreach (var profile in profiles)
            {
                Console.WriteLine($"Id: {profile.Id}, Username: {profile.Username}, Email: {profile.Email}");
            }


            //Console.WriteLine("\nFrom Database:");
            //var dbProfiles = await _profileService.GetProfilesFromDb();
            //foreach (var profile in dbProfiles)
            //{
            //    Console.WriteLine(profile.ToString());
            //}
        }
    }
}
