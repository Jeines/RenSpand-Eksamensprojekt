using RenSpand_Eksamensprojekt;
using System.Linq.Expressions;

namespace RenspandWebsite.Service
{
    public class ProfileService
    {
        public List<Profile> Profiles { get; set; }
        private JsonFileService<Profile> JsonFileService { get; set; }

        public ProfileService(JsonFileService<Profile> jsonFileService)
        {
            JsonFileService = jsonFileService;
            Profiles = JsonFileService.GetJsonObjects().ToList();
            jsonFileService.SaveJsonObjects(Profiles);
        }

        /// <summary>
        /// Adds profile to the list of profiles and saves it to the JSON file.
        /// </summary>
        /// <param name="profile"></param>
        public void AddProfile(Profile profile)
        {
            Profiles.Add(profile);
            JsonFileService.SaveJsonObjects(Profiles);
        }
    }
}
