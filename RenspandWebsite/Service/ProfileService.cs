using Microsoft.AspNetCore.Identity;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;
using System.Linq.Expressions;

namespace RenspandWebsite.Service
{
    public class ProfileService
    {
        public List<Profile> Profiles { get; set; }
        private JsonFileService<Profile> _jsonFileService;
        private ProfileDbService _userDbService;
        private PasswordHasher<string> passwordHasher;

        public ProfileService(JsonFileService<Profile> jsonFileService, ProfileDbService dbService)
        {
            _jsonFileService = jsonFileService;
            _userDbService = dbService;
            Profiles = _userDbService.GetObjectsAsync().Result.ToList();
            //Profiles = _jsonFileService.GetJsonObjects().ToList();
            foreach (Profile profile in _jsonFileService.GetJsonObjects().ToList())
            { Profiles.Add(profile); }
            //_jsonFileService.SaveJsonObjects(Profiles);
            //_userDbService.SaveUserObjects(Profiles);
        }

        public string GetPassword(int id, string password)
        {
            Profiles = _userDbService.GetObjectsAsync().Result.ToList();
            // Find the profile with the given username
            Profile profile = Profiles.FirstOrDefault(p => p.Id == id);
            if (profile != null)
            {
                //var passwordHasher = new PasswordHasher<string>();
                //if (passwordHasher.VerifyHashedPassword(null, profile.Password, password) == PasswordVerificationResult.Success)

                return profile.Password;
            }
            return null; // or throw an exception if you prefer
        }


        public Profile GetUserData(int id)
        {
            Profile selectedProfile = new Profile();
            foreach (Profile p in _userDbService.GetObjectsAsync().Result.ToList())
            {
                if (p.Id == id) selectedProfile = p;
            }
            return selectedProfile;
        }

        public void UpdateUserData(int id, Profile profile)
        {
            // Find the profile with the given ID
            Profile existingProfile = Profiles.FirstOrDefault(p => p.Id == id);
            if (existingProfile != null)
            {
                existingProfile.Password = profile.Password;
                existingProfile.Username = profile.Username;
                existingProfile.Email = profile.Email;
                existingProfile.PhoneNumber = profile.PhoneNumber;
                existingProfile.Name = profile.Name;
                existingProfile.Address = profile.Address;
                existingProfile.Role = profile.Role;

                _userDbService.UpdateObjectAsync(id, existingProfile).Wait();
            }

            // Refresh in-memory list
            Profiles = _userDbService.GetObjectsAsync().Result.ToList();
        }


        /// <summary>
        /// Adds profile to the list of profiles and saves it to the JSON file.
        /// </summary>
        /// <param name="profile"></param>
        public void AddProfile(Profile profile)
        {
            Profiles.Add(profile);
            //_jsonFileService.SaveJsonObjects(Profiles);
            _userDbService.AddObjectAsync(profile).Wait();           
        }


        public void UpdatePassWord(int id, string newPassword)
        {
            passwordHasher = new PasswordHasher<string>();
            var hashedPassword = passwordHasher.HashPassword(null, newPassword);

            _userDbService.UpdatePasswordAsync(id, hashedPassword).Wait();

            // Refresh in-memory list
            Profiles = _userDbService.GetObjectsAsync().Result.ToList();
        }


        public async Task<List<Order>> GetUserOrders(int userId)
        {
            return await _userDbService.GetOrdersByIdAsync(userId);
        }

        //method to add default address to the database so you can add a profile to without an address
        public async Task AddTestAddressAsync()
        {
            // Somewhere in your service or OnGetAsync
            using (var context = new RenSpandDbContext())
            {
                var address = new Address
                {
                    Street = "Test Street",
                    ZipCode = "1234",
                    City = "Testville"
                };
                context.Addresses.Add(address);
                await context.SaveChangesAsync();
            }
        }


    }
}
