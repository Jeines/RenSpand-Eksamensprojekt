using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;

namespace RenspandWebsite.Service
{
    /// <summary>
    /// Serviceklasse for håndtering af forretningslogik relateret til ordrer.
    /// </summary>
    public class OrderService
    {
        private readonly OrderDbService _orderDbService;

        // Constructor, der tager OrderDbService som parameter for at interagere med databasen.
        public OrderService(OrderDbService orderDbService)
        {
            _orderDbService = orderDbService;
        }

        /// <summary>
        /// Opretter en ny ordre baseret på de oplysninger, der gives.
        /// Denne metode indeholder forretningslogik som prisberegning.
        /// </summary>
        public async Task<Order> CreateOrderAsync(
        string name, string email, string phonenumber,
        string street, string city, string zipcode,
        List<int[]> workAndAmount, DateTime datestart, DateTime trashcanemptydate)
        {
            using var context = new RenSpandDbContext();

            // Beregn prisen baseret på arbejdet og mængden
            decimal totalPrice = CalculateTotalPrice(workAndAmount);

            // 1. Opretter og gemmer en ny bruger
            var user = new User
            {
                Role = RoleEnum.Guest,
                Name = name,
                Email = email,
                PhoneNumber = phonenumber
            };
            context.Users.Add(user);
            await context.SaveChangesAsync(); // Gemmer brugeren

            // 2. Opretter og gemmer en ny adresse
            var address = new Address
            {
                Street = street,
                City = city,
                ZipCode = zipcode
            };
            context.Addresses.Add(address);
            await context.SaveChangesAsync(); // Gemmer adressen

            // 3. Opretter og gemmer en ny ordre
            var order = new Order
            {
                BuyerId = user.Id, // Brugerens ID tilknyttes ordren
                Buyer = user, // Relaterer ordren til brugeren
                TotalPrice = totalPrice, // Tilføjer den beregnede pris
                DateStart = datestart,
                DateDone = datestart.AddDays(8),  // Beregner slutdato
                TrashCanEmptyDate = trashcanemptydate
            };
            context.Orders.Add(order);
            await context.SaveChangesAsync(); // Gemmer ordren

            // 4. Opretter og gemmer et AddressItem for at forbinde adressen med ordren
            var addressItem = new AddressItem
            {
                OrderId = order.Id, // Relaterer ordren til adressen
                Address = address
            };
            context.AddressItems.Add(addressItem);
            await context.SaveChangesAsync(); // Gemmer AddressItem

            // 5. Opretter og gemmer et ServiceItem for hver work
            foreach (var entry in workAndAmount)
            {
                int workId = entry[0];
                int amount = entry[1];

                // Henter arbejdet baseret på workId
                var work = await context.Works.FindAsync(workId) ?? throw new Exception($"Work with ID {workId} not found.");

                // Opretter og gemmer ServiceItem
                var workItem = new ServiceItem
                {
                    OrderId = order.Id,  // Relaterer arbejdet til ordren
                    ServiceWork = work,   // Relaterer arbejdet til serviceitem
                    Amount = amount       // Angiver mængden af arbejdet
                };

                context.ServiceItems.Add(workItem);
            }

            await context.SaveChangesAsync(); // Gemmer alle ServiceItems

            // Returnerer den oprettede ordre, som nu inkluderer ID
            return order;
        }


        /// <summary>
        /// Beregner den samlede pris for en ordre baseret på arbejdet og mængden.
        /// </summary>
        private decimal CalculateTotalPrice(List<int[]> workAndAmount)
        {
            decimal totalPrice = 0;

            // Antag at du har adgang til en liste af works, her skal du hente priserne på de forskellige works.
            // (F.eks. fra en database eller en hardcoded liste)
            foreach (var entry in workAndAmount)
            {
                int workId = entry[0];
                int amount = entry[1];

                // Find arbejdet og beregn prisen
                // Her simulerer vi en fast pris per enhed, for demo
                decimal pricePerWork = 100m;  // Fastsat pris pr. arbejde
                totalPrice += pricePerWork * amount;
            }

            return totalPrice;
        }

        /// <summary>
        /// Henter alle ordrer fra databasen.
        /// </summary>
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderDbService.GetAllOrdersAsync();
        }

        /// <summary>
        /// Henter en specifik ordre baseret på ID.
        /// </summary>
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderDbService.GetOrderByIdAsync(id);
        }

        /// <summary>
        /// Opdaterer en eksisterende ordre.
        /// </summary>
        public async Task UpdateOrderAsync(Order order)
        {
            await _orderDbService.UpdateOrderAsync(order);
        }

        /// <summary>
        /// Sletter en ordre baseret på ID.
        /// </summary>
        public async Task DeleteOrderAsync(int id)
        {
            await _orderDbService.DeleteOrderAsync(id);
        }

        /// <summary>
        /// Accepterer en ordre baseret på ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AcceptOrderAsync(int id)
        {
            var order = await _orderDbService.GetOrderByIdAsync(id);
            if (order == null)
                throw new Exception($"Order with ID {id} not found.");

            order.AcceptStatus = AcceptStatusEnum.Accepted;
            await _orderDbService.UpdateOrderAsync(order);
        }

        /// <summary>
        /// Afviser en ordre baseret på ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task RejectOrderAsync(int id)
        {
            var order = await _orderDbService.GetOrderByIdAsync(id);
            if (order == null)
                throw new Exception($"Order with ID {id} not found.");

            order.AcceptStatus = AcceptStatusEnum.Rejected;
            await _orderDbService.UpdateOrderAsync(order);
        }

        /// <summary>
        /// Gemmer en note til en ordre baseret på ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SaveNoteAsync(int id, string note)
        {
            var order = await _orderDbService.GetOrderByIdAsync(id);
            if (order == null)
                throw new Exception($"Order with ID {id} not found.");

            order.EmployeeNote = note;
            await _orderDbService.UpdateOrderAsync(order);
        }

        /// <summary>
        /// Henter alle works fra databasen.
        /// </summary>
        /// <param name="workId"></param>
        /// <returns></returns>
        public async Task<Work> GetWorkByIdAsync(int workId)
        {
            using var context = new RenSpandDbContext();
            return await context.Works.FindAsync(workId);
        }


    }
}
