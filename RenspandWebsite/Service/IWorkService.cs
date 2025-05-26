using RenspandWebsite.Models;

namespace RenspandWebsite.Service
{
    /// <summary>
    /// Interface til håndtering af Work-objekter.
    /// Indeholder metoder til at hente, tilføje, opdatere, slette og filtrere Work.
    /// </summary>
    public interface IWorkService
    {
        /// <summary>
        /// Henter alle Work-objekter.
        /// </summary>
        /// <returns>Liste af Work.</returns>
        List<Work> GetWorks();

        /// <summary>
        /// Tilføjer et nyt Work-objekt.
        /// </summary>
        /// <param name="work">Work-objektet der skal tilføjes.</param>
        void AddWork(Work work);

        /// <summary>
        /// Opdaterer et eksisterende Work-objekt.
        /// </summary>
        /// <param name="work">Work-objektet der skal opdateres.</param>
        void UpdateWork(Work work);

        /// <summary>
        /// Henter et Work-objekt ud fra dets id.
        /// </summary>
        /// <param name="id">Id på det ønskede Work-objekt.</param>
        /// <returns>Work-objektet hvis det findes, ellers null.</returns>
        Work GetWork(int id);

        /// <summary>
        /// Sletter et Work-objekt ud fra dets id.
        /// </summary>
        /// <param name="workId">Id på det Work-objekt der skal slettes.</param>
        /// <returns>Det slettede Work-objekt hvis det fandtes, ellers null.</returns>
        Work DeleteWork(int? workId);

        /// <summary>
        /// Filtrerer Work-objekter baseret på prisinterval.
        /// </summary>
        /// <param name="maxPrice">Maksimal pris.</param>
        /// <param name="minPrice">Minimal pris (standardværdi er 0).</param>
        /// <returns>Enumerable af Work-objekter inden for prisintervallet.</returns>
        IEnumerable<Work> PriceFilter(int maxPrice, int minPrice = 0);
    }
}
