using RenspandWebsite.Models;
using RenspandWebsite.Service.ProfileServices;

namespace RenspandWebsite.Service.WorkServices
{
    /// <summary>
    /// Serviceklasse til håndtering af Work-objekter.
    /// Indeholder metoder til CRUD-operationer og prisfiltrering.
    /// </summary>
    public class WorkService
    {
        private readonly WorkDbService _workDbService;

        /// <summary>
        /// Initialiserer en ny instans af WorkService.
        /// </summary>
        /// <param name="workDbService">Database service til Work-objekter.</param>
        public WorkService(WorkDbService workDbService)
        {
            _workDbService = workDbService;
        }

        /// <summary>
        /// Henter alle Work-objekter fra databasen.
        /// </summary>
        /// <returns>Liste af Work-objekter.</returns>
        
        public async Task<IEnumerable<Work>> GetWorks()
        {
            return await _workDbService.GetObjectsAsync();
        }

        /// <summary>
        /// Henter et Work-objekt ud fra dets id.
        /// </summary>
        /// <param name="id">Id på Work-objektet.</param>
        /// <returns>Work-objektet.</returns>
        public async Task<Work> GetWork(int id)
        {
            return await _workDbService.GetObjectByIdAsync(id);
        }

        /// <summary>
        /// Opdaterer et eksisterende Work-objekt i databasen.
        /// </summary>
        /// <param name="work">Work-objektet der skal opdateres.</param>
        public async Task UpdateWork(Work work)
        {
            await _workDbService.UpdateObjectAsync(work);
        }

        /// <summary>
        /// Tilføjer et nyt Work-objekt til databasen.
        /// </summary>
        /// <param name="work">Work-objektet der skal tilføjes.</param>
        public async Task AddWork(Work work)
        {
            await _workDbService.AddObjectAsync(work);
        }

        /// <summary>
        /// Sletter et Work-objekt fra databasen ud fra dets id.
        /// </summary>
        /// <param name="id">Id på Work-objektet der skal slettes.</param>
        public async Task DeleteWork(int id)
        {
           await _workDbService.DeleteObjectAsync(id);
        }

        /// <summary>
        /// Filtrerer Work-objekter baseret på prisinterval.
        /// </summary>
        /// <param name="maxPrice">Maksimal pris.</param>
        /// <param name="minPrice">Minimal pris.</param>
        /// <returns>Liste af Work-objekter indenfor prisintervallet.</returns>
        public List<Work> PriceFilter(int maxPrice, int minPrice)
        {
            var allProducts = _workDbService.GetObjectsAsync().Result.ToList();
            var filteredProducts = allProducts.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
            return filteredProducts;
        }
    }
}
