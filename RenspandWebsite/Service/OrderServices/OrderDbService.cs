using Microsoft.EntityFrameworkCore;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;

namespace RenspandWebsite.Service.OrderServices
{
    public class OrderDbService : DbService<Order>
    {
        /// <summary>
        /// Henter alle ordrer fra databasen inklusiv relaterede Buyer, AddressItems og ServiceItems.
        /// </summary>
        /// <returns>Liste af ordrer med tilhørende data</returns>
        public async Task<List<Order>> GetOrdersWithJoinsAsync()
        {
            using var context = new RenSpandDbContext();

            // Først hentes alle ordrer med tilhørende køber
            var orders = await context.Orders
                .Include(o => o.Buyer)
                .ToListAsync();

            // Hent alle relaterede AddressItems og ServiceItems baseret på OrderId
            var orderIds = orders.Select(o => o.Id).ToList();

            var addressItems = await context.AddressItems
                .Include(ai => ai.Address)
                .Where(ai => orderIds.Contains(ai.OrderId))
                .ToListAsync();

            var serviceItems = await context.ServiceItems
                .Include(si => si.ServiceWork)
                .Where(si => orderIds.Contains(si.OrderId))
                .ToListAsync();

            // Matcher AddressItems og ServiceItems tilbage til ordrerne
            foreach (var order in orders)
            {
                order.AddressItems = addressItems.Where(ai => ai.OrderId == order.Id).ToList();
                order.ServiceItems = serviceItems.Where(si => si.OrderId == order.Id).ToList();
            }
            return orders;
        }

        //TODO: Tilføj metode til at oprette ordre med en logget ind bruger
        /// <summary>
        /// Opretter en ny ordre i databasen.
        /// </summary>
        /// <param name="name">Navn på køber</param>
        /// <param name="email">Email på køber</param>
        /// <param name="phonenumber">Telefonnummer på køber</param>
        /// <param name="street">Vejnavn på købers adresse</param>
        /// <param name="city">By på købers adresse</param>
        /// <param name="zipcode">Postnummer på købers adresse</param>
        /// <param name="workAndAmount">Liste med hvilke og hvor mange opgaver der er bestilt</param>
        /// <param name="datestart">Dato hvor ordren starter</param>
        /// <param name="trashcanemptydate">Dato hvor skraldespanden tømmes</param>
        /// <param name="totalPrice">Samlet pris for ordren</param>
        /// <param name="customerNote">Evt. kommentar fra kunden</param>
        /// <returns>Ordre fra databasen med sit Id</returns>
        public async Task<Order> CreateFullOrderAsync(
            string name, string email, string phonenumber,
            string street, string city, string zipcode,
            List<int[]> workAndAmount,
            DateTime datestart, DateTime trashcanemptydate, decimal totalPrice, string customerNote)
        {
            // Opretter en ny instans af RenSpandDbContext (forbindelse til database)
            using var context = new RenSpandDbContext();

            // 1. Opretter en ny bruger og gemmer den i databasen
            var user = new User
            {
                Role = RoleEnum.Guest,
                Name = name,
                Email = email,
                PhoneNumber = phonenumber
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            // 2. Opretter en ny adresse og gemmer den i databasen
            var address = new Address
            {
                Street = street,
                City = city,
                ZipCode = zipcode
            };
            context.Addresses.Add(address);
            await context.SaveChangesAsync();

            // 3. Opretter en ny ordre og gemmer den i databasen
            var order = new Order
            {
                Buyer = user,
                DateStart = datestart,
                DateDone = datestart.AddDays(8),
                TrashCanEmptyDate = trashcanemptydate,
                TotalPrice = totalPrice,
                CustomerNote = customerNote
            };
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            // 4. Opretter AddressItem og gemmer den i databasen
            var addressItem = new AddressItem
            {
                OrderId = order.Id,
                Address = address
            };
            context.AddressItems.Add(addressItem);
            await context.SaveChangesAsync();

            // 5. Opretter ServiceItem for hver opgave og gemmer dem i databasen
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
        /// Henter alle opgaver (works) fra databasen.
        /// </summary>
        /// <returns>Liste af opgaver</returns>
        public async Task<List<Work>> GetAllWorksAsync()
        {
            using var context = new RenSpandDbContext();
            List<Work>? works = await context.Works.ToListAsync();
            return works;
        }
    }
}
