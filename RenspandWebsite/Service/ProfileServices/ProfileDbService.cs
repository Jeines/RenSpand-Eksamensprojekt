using Microsoft.EntityFrameworkCore;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;

namespace RenspandWebsite.Service.ProfileServices
{
    public class ProfileDbService : DbService<Profile>
    {
        /// <summary>
        /// Henter alle ordrer fra databasen for en given bruger.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Order>> GetOrdersByIdAsync(int userId)
        {
            using var context = new RenSpandDbContext();
            return await context.Orders
            .Where(order => order.Buyer.Id == userId)
            .Include(order => order.Buyer)
            .ToListAsync();
        }
    }
}
