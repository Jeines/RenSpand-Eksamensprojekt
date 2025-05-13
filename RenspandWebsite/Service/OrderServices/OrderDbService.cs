using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RenspandWebsite.EFDbContext;
using RenSpand_Eksamensprojekt;

namespace RenspandWebsite.Service
{
    /// <summary>
    /// Serviceklasse for håndtering af databaseoperationer for ordrer.
    /// Denne klasse bruger den generiske DbService til at håndtere CRUD-operationer.
    /// </summary>
    public class OrderDbService : DbService<Order>
    {
        // Constructor, der initialiserer OrderDbService med en instans af RenSpandDbContext
        public OrderDbService(RenSpandDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Henter alle ordrer fra databasen.
        /// </summary>
        /// <returns>En liste af ordrer.</returns>
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            // Bruger den generiske metode fra DbService til at hente ordrer
            return (List<Order>)await GetObjectsAsync();
        }

        /// <summary>
        /// Henter en ordre fra databasen baseret på ID.
        /// </summary>
        /// <param name="id">ID for ordren.</param>
        /// <returns>En ordre, hvis den findes, ellers null.</returns>
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            // Bruger den generiske metode fra DbService til at hente en ordre
            return await GetObjectByIdAsync(id);
        }

        /// <summary>
        /// Opretter en ny ordre i databasen.
        /// </summary>
        /// <param name="order">Ordren der skal oprettes.</param>
        /// <returns>Den oprettede ordre.</returns>
        public async Task<Order> AddOrderAsync(Order order)
        {
            // Bruger den generiske metode fra DbService til at tilføje en ordre
            return await AddObjectAsync(order);
        }

        /// <summary>
        /// Opdaterer en eksisterende ordre i databasen.
        /// </summary>
        /// <param name="order">Ordren der skal opdateres.</param>
        public async Task UpdateOrderAsync(Order order)
        {
            // Bruger den generiske metode fra DbService til at opdatere en ordre
            await UpdateObjectAsync(order);
        }

        /// <summary>
        /// Sletter en ordre fra databasen baseret på ID.
        /// </summary>
        /// <param name="id">ID for ordren, der skal slettes.</param>
        public async Task DeleteOrderAsync(int id)
        {
            // Bruger den generiske metode fra DbService til at slette en ordre
            await DeleteObjectAsync(id);
        }
    }
}
