using Microsoft.EntityFrameworkCore;
using RenspandWebsite.EFDbContext;
using RenspandWebsite.Models;

namespace RenspandWebsite.Service.AboutUsService
{
    /// <summary>
    /// Serviceklasse til databaseoperationer for AboutUs-objekter.
    /// Arver fra DbService og tilbyder metoder til at hente AboutUs-data fra databasen.
    /// </summary>
    public class AboutUsDbServices : DbService<AboutUs>
    {
        /// <summary>
        /// Henter et AboutUs-objekt fra databasen ud fra dets ID.
        /// </summary>
        /// <param name="id">ID for det ønskede AboutUs-objekt.</param>
        /// <returns>Det fundne AboutUs-objekt eller null hvis det ikke findes.</returns>
        public async Task<AboutUs> GetAboutUsByIdAsync(int id)
        {
            using var context = new RenSpandDbContext();
            //return await context.Profiles
            //        .Include(p => p.Address)
            //        .FirstOrDefaultAsync(p => p.Id == id);

            return await context.AboutUss
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
        
}
