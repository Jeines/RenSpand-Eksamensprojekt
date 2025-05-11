using Microsoft.EntityFrameworkCore;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;

namespace RenspandWebsite.Service.CreateOrderServices
{
    public class CreateOrderDbService : DbService<Order>
    {

        /// <summary>
        /// Creates a full order including User, Address, and Order.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="phonenumber"></param>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="zipcode"></param>
        /// <param name="work"></param>
        /// <param name="workamount"></param>
        /// <param name="datestart"></param>
        /// <param name="trashcanemptydate"></param>
        /// <returns></returns>
        public async Task<Order> CreateFullOrderAsync(
            string name, string email, string phonenumber,
            string street, string city, string zipcode,
            Work work, int workamount,
            DateTime datestart, DateTime trashcanemptydate)
        {
            using var context = new RenSpandDbContext();

            // 1. Create and save User
            var user = new User
            {
                Name = name,
                Email = email,
                PhoneNumber = phonenumber
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            // 2. Create and save Address
            var address = new Address
            {
                Street = street,
                City = city,
                ZipCode = zipcode
            };
            context.Addresses.Add(address);
            await context.SaveChangesAsync();

            // 3. Calculate price and create Order
            decimal totalPrice = work.Price * workamount;
            var order = new Order
            {
                Buyer = user,
                TotalPrice = totalPrice,
                DateStart = datestart,
                DateDone = datestart.AddDays(8),
                TrashCanEmptyDate = trashcanemptydate
            };
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            // 4. Create AddressItem
            var addressItem = new AddressItem
            {
                OrderId = order.Id,
                Address = address
            };
            context.AddressItems.Add(addressItem);
            await context.SaveChangesAsync();

            context.Works.Attach(work);

            // 5. Create WorkItem
            var workItem = new ServiceItem
            {
                OrderId = order.Id,
                ServiceWork = work,
                Amount = workamount
            };
            Console.WriteLine("OrderId: "+order + "ServiceWork: " + work + "Amount: " + workamount);
            context.ServiceItems.Add(workItem);
            await context.SaveChangesAsync();

            return order; // now includes Id
        }


        public async Task<List<Work>> GetAllWorksAsync()
        {
            using var context = new RenSpandDbContext();
            List<Work>? works = await context.Works.ToListAsync();
            return works;
        }

        public async Task SaveOrderObjects(IEnumerable<Order> orders)
        {
            using var context = new RenSpandDbContext();
            //adds json profiles to the database
            foreach (var order in orders)
            {
                order.Id = 0; // Reset the ID to 0 before saving to DB
                AddObjectAsync(order).Wait(); // Wait for the task to complete
            }
        }
    }
}
