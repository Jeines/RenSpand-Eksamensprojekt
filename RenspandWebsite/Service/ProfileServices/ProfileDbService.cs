using Microsoft.EntityFrameworkCore;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;

namespace RenspandWebsite.Service.ProfileServices
{
    public class ProfileDbService : DbService<Profile>
    {
        public ProfileDbService(RenSpandDbContext context) : base(context)
        {
        }
        //TODO: add comment to the methods



        public async Task<List<Order>> GetOrdersByIdAsync(int userId)
        {
            using var context = new RenSpandDbContext();
            return await context.Orders
            .Where(order => order.Buyer.Id == userId)
            .Include(order => order.Buyer)
            .ToListAsync();
        }

        public async Task SaveUserObjects(IEnumerable<Profile> profiles)
        {
            using var context = new RenSpandDbContext();
            //adds json profiles to the database
            foreach (var profile in profiles)
            {
                profile.Id = 0; // Reset the ID to 0 before saving to DB
                AddObjectAsync(profile).Wait(); // Wait for the task to complete
            }
        }
    }
}


