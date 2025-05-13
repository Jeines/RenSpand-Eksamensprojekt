using Microsoft.EntityFrameworkCore;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;

namespace RenspandWebsite.Service.OrderServices
{
    public class OrderDbService : DbService<Order>
    {

        public async Task<List<Order>> GetOrdersWithJoinsAsync()
        {
            using var context = new RenSpandDbContext();

            // First, load all Orders with Buyer included
            var orders = await context.Orders
                .Include(o => o.Buyer)
                .ToListAsync();

            // Get all related AddressItems and ServiceItems by OrderId
            var orderIds = orders.Select(o => o.Id).ToList();

            var addressItems = await context.AddressItems
                .Include(ai => ai.Address)
                .Where(ai => orderIds.Contains(ai.OrderId))
                .ToListAsync();

            var serviceItems = await context.ServiceItems
                .Include(si => si.ServiceWork)
                .Where(si => orderIds.Contains(si.OrderId))
                .ToListAsync();

            // Match AddressItems and ServiceItems back to Orders
            foreach (var order in orders)
            {
                order.AddressItems = addressItems.Where(ai => ai.OrderId == order.Id).ToList();
                order.ServiceItems = serviceItems.Where(si => si.OrderId == order.Id).ToList();
            }

            return orders;
        }





        //public async Task<List<Order>> GetOrdersByIdAsync(int userId)
        //{
        //    using var context = new RenSpandDbContext();
        //    return await context.Orders
        //    .Where(order => order.Buyer.Id == userId)
        //    .Include(order => order.Buyer)
        //    .ToListAsync();
        //}

        //public async Task SaveOrderObjects(IEnumerable<Profile> profiles)
        //{
        //    using var context = new RenSpandDbContext();
        //    //adds json profiles to the database
        //    foreach (var profile in profiles)
        //    {
        //        profile.Id = 0; // Reset the ID to 0 before saving to DB
        //        AddObjectAsync(profile).Wait(); // Wait for the task to complete
        //    }
        //}
    }
}
