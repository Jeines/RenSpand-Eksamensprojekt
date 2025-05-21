using Microsoft.EntityFrameworkCore;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;

namespace RenspandWebsite.Service.FaqServices
{
    /// <summary>
    /// Serviceklasse til håndtering af FAQ-objekter i databasen.
    /// Indeholder metoder til at hente, tilføje, opdatere og slette FAQ'er.
    /// </summary>
    public class FaqDbService : DbService<FAQ>
    {
        /// <summary>
        /// Henter alle FAQ-objekter fra databasen.
        /// </summary>
        /// <returns>Liste af FAQ-objekter</returns>
        public async Task<List<FAQ>> GetAllFAQsAsync()
        {
            using var context = new RenSpandDbContext();
            return await context.FAQs.ToListAsync();
        }

        /// <summary>
        /// Henter et FAQ-objekt ud fra dets ID.
        /// </summary>
        /// <param name="id">FAQ'ens ID</param>
        /// <returns>FAQ-objekt eller null hvis ikke fundet</returns>
        public async Task<FAQ> GetFAQByIdAsync(RenSpandDbContext context,int id)
        {
            
            return await context.FAQs.FindAsync(id);
        }

        /// <summary>
        /// Tilføjer et nyt FAQ-objekt til databasen.
        /// </summary>
        /// <param name="question">Spørgsmålet</param>
        /// <param name="answer">Svaret</param>
        public async Task AddFAQAsync(string question, string answer)
        {
            using var context = new RenSpandDbContext();
            var newFAQ = new FAQ
            {
                Question = question,
                Answer = answer
            };
            context.FAQs.Add(newFAQ);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Opdaterer et eksisterende FAQ-objekt i databasen.
        /// </summary>
        /// <param name="id">FAQ'ens ID</param>
        /// <param name="question">Nyt spørgsmål</param>
        /// <param name="answer">Nyt svar</param>
        public async Task UpdateFAQAsync(int id, string question, string answer)
        {
            using var context = new RenSpandDbContext();
            var faq = await GetFAQByIdAsync(context,id);
            if (faq != null)
            {
                faq.Question = question;
                faq.Answer = answer;
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Sletter et FAQ-objekt fra databasen ud fra dets ID.
        /// </summary>
        /// <param name="id">FAQ'ens ID</param>
        public async Task DeleteFAQAsync(int id)
        {
            using var context = new RenSpandDbContext();
            var faq = await GetFAQByIdAsync(context, id);
            if (faq != null)
            {
                context.FAQs.Remove(faq);
                await context.SaveChangesAsync();
            }
        }
    }
}
