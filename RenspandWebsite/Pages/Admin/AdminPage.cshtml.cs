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
            //Console.WriteLine("\nAdding test profile to DB...");
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
