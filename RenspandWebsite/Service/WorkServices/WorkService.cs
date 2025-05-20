using RenSpand_Eksamensprojekt;
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
        public List<Work> GetWorks()
        {
            return _workDbService.GetObjectsAsync().Result.ToList();
        }

        /// <summary>
        /// Henter et Work-objekt ud fra dets id.
        /// </summary>
        /// <param name="id">Id på Work-objektet.</param>
        /// <returns>Work-objektet.</returns>
        public Work GetWork(int id)
        {
            return _workDbService.GetObjectByIdAsync(id).Result;
        }

        /// <summary>
        /// Opdaterer et eksisterende Work-objekt i databasen.
        /// </summary>
        /// <param name="work">Work-objektet der skal opdateres.</param>
        public void UpdateWork(Work work)
        {
            _workDbService.UpdateObjectAsync(work).Wait();
        }

        /// <summary>
        /// Tilføjer et nyt Work-objekt til databasen.
        /// </summary>
        /// <param name="work">Work-objektet der skal tilføjes.</param>
        public void AddWork(Work work)
        {
            _workDbService.AddObjectAsync(work).Wait();
        }

        /// <summary>
        /// Sletter et Work-objekt fra databasen ud fra dets id.
        /// </summary>
        /// <param name="id">Id på Work-objektet der skal slettes.</param>
        public void DeleteWork(int id)
        {
            _workDbService.DeleteObjectAsync(id).Wait();
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
