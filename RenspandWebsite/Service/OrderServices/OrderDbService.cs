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
    }
}
