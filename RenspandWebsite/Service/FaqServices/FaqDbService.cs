using Microsoft.EntityFrameworkCore;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;

namespace RenspandWebsite.Service.FaqServices
{
    public class FaqDbService: DbService<FAQ>
    {
        public async Task<List<FAQ>> GetAllFAQsAsync()
        {
            using var context = new RenSpandDbContext();
            return await context.FAQs.ToListAsync();
        }
        public async Task<FAQ> GetFAQByIdAsync(int id)
        {
            using var context = new RenSpandDbContext();
            return await context.FAQs.FindAsync(id);
        }
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
        public async Task UpdateFAQAsync(int id, string question, string answer)
        {
            using var context = new RenSpandDbContext();
            var faq = await GetFAQByIdAsync(id);
            if (faq != null)
            {
                faq.Question = question;
                faq.Answer = answer;
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteFAQAsync(int id)
        {
            using var context = new RenSpandDbContext();
            var faq = await GetFAQByIdAsync(id);
            if (faq != null)
            {
                context.FAQs.Remove(faq);
                await context.SaveChangesAsync();
            }
        }
    
    }
}
