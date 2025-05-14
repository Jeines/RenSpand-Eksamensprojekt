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


        //TODO: Add method to make order with a logged in user
        /// <summary>
        /// Laver en ny ordre i databasen.
        /// </summary>
        /// <param name="name">Navn på køber</param>
        /// <param name="email">Email på køber</param>
        /// <param name="phonenumber">TelefonNummer på køber</param>
        /// <param name="street">Vejnavn på køber addresse</param>
        /// <param name="city">By på køber addresse</param>
        /// <param name="zipcode">PostNummer på køber addresse</param>
        /// <param name="workAndAmount">Listen med hvilket og hvor meget arbejde der er bestilt</param>
        /// <param name="datestart">Dato'en ordren er købt fra</param>
        /// <param name="trashcanemptydate">Dato'en køber får tømt skraldespand</param>
        /// <returns>Ordre fra databasen med sit Id</returns>
        public async Task<Order> CreateFullOrderAsync(
            string name, string email, string phonenumber,
            string street, string city, string zipcode,
            List<int[]> workAndAmount,
            DateTime datestart, DateTime trashcanemptydate, decimal totalPrice)
        {
            // Laver en ny instans af RenSpandDbContext (forbindelse til database)
            using var context = new RenSpandDbContext();

            // 1. Laver en ny bruger og gemmer den i databasen
            var user = new User
            {
                Role = RoleEnum.Guest,
                Name = name,
                Email = email,
                PhoneNumber = phonenumber
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            // 2. Laver en ny adresse og gemmer den i databasen
            var address = new Address
            {
                Street = street,
                City = city,
                ZipCode = zipcode
            };
            context.Addresses.Add(address);
            await context.SaveChangesAsync();

            // 3. Laver en ny ordre og gemmer den i databasen
            var order = new Order
            {
                Buyer = user,
                DateStart = datestart,
                DateDone = datestart.AddDays(8),
                TrashCanEmptyDate = trashcanemptydate,
                TotalPrice = totalPrice
            };
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            // 4. Laver AddressItem og gemmer den i databasen
            var addressItem = new AddressItem
            {
                OrderId = order.Id,
                Address = address
            };
            context.AddressItems.Add(addressItem);
            await context.SaveChangesAsync();

            // 5. Laver ServiceItem for hver work og gemmer dem i databasen
            var serviceItems = new List<ServiceItem>();
            foreach (var entry in workAndAmount)
            {
                int workId = entry[0];
                int amount = entry[1];

                serviceItems.Add(new ServiceItem
                {
                    OrderId = order.Id,
                    ServiceWorkId = workId,
                    Amount = amount
                });
            }

            // 6. Gemmer alle ændringer i databasen
            context.ServiceItems.AddRange(serviceItems);
            await context.SaveChangesAsync();
            return order;
        }

        /// <summary>
        /// Henter alle works fra databasen.
        /// </summary>
        /// <returns>Liste af Works</returns>
        public async Task<List<Work>> GetAllWorksAsync()
        {
            using var context = new RenSpandDbContext();
            List<Work>? works = await context.Works.ToListAsync();
            return works;
        }


    }
}
