using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;
using System.Linq.Expressions;

namespace RenspandWebsite.Service
{
    public class ProfileService
    {
        public List<Profile> Profiles { get; }
        private readonly JsonFileService<Profile> _jsonFileService;

        //TODO: Update Name
        private readonly ProfileDbService _userDbService;

        public ProfileService(JsonFileService<Profile> jsonFileService, ProfileDbService dbService)
        {
            _jsonFileService = jsonFileService;
            _userDbService = dbService;
            //Profiles = _userDbService.GetObjectsAsync().Result.ToList();
            Profiles = _jsonFileService.GetJsonObjects().ToList();
            //_jsonFileService.SaveJsonObjects(Profiles);
            //_userDbService.SaveUserObjects(Profiles);
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

        public async Task<List<Order>> GetUserOrders(int userId)
        {
            return await _userDbService.GetOrdersByIdAsync(userId);
        }

        //method to add default address to the database so you can add a profile to without an address
        public async Task AddTestAddressAsync()
        {
            // Somewhere in your service or OnGetAsync
            using var context = new RenSpandDbContext();
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
