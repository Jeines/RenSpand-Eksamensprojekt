using Microsoft.EntityFrameworkCore;
using RenspandWebsite.Models;
using RenspandWebsite.EFDbContext;


namespace RenspandWebsite.Service.FaqServices
{

    public class FaqService
    { 
        private readonly DbService<FAQ> _faqDbService;
        
        public List<FAQ> FAQs { get; }
        private readonly List<FAQ> _faqs;
       

        public FaqService(DbService<FAQ> faqDbService)
        {
            _faqDbService = faqDbService;
            _faqs = new List<FAQ>();
            FAQs = _faqs;
        }

        /// <summary>
        /// Henter alle FAQs
        /// </summary>
        public async Task<IEnumerable<FAQ>> GetFAQsAsync()
        {
            return await _faqDbService.GetObjectsAsync();
        }

        /// <summary>
        /// Henter en FAQ baseret på ID
        /// </summary>
        public async Task<FAQ> GetFAQByIdAsync(int id)
        {
            return await _faqDbService.GetObjectByIdAsync(id);
        }

        /// <summary>
        /// Tilføjer en ny FAQ og opdaterer listen
        /// </summary>
        public async Task AddFAQAsync(string question, string answer)
        {
           var newFAQ = new FAQ { Question = question, Answer = answer };
           await _faqDbService.AddObjectAsync(newFAQ);
        }

        /// <summary>
        /// Opdaterer en eksisterende FAQ og opdaterer listen
        /// </summary>
        public async Task UpdateFAQAsync(FAQ UpdatedFAQ)
        {
            await _faqDbService.UpdateObjectAsync(UpdatedFAQ);
            RefreshFAQListAsync();
        }
       

        /// <summary>
        /// Sletter en FAQ og opdaterer listen
        /// </summary>
        public async Task DeleteFAQ(int id)
        {
            await _faqDbService.DeleteObjectAsync(id);
        }

        /// <summary>
        /// Opdaterer den interne liste med de nyeste data
        /// </summary>
        private async Task RefreshFAQListAsync()
        {
            _faqs.Clear();
            var faqsFromDb = await _faqDbService.GetObjectsAsync();
            _faqs.AddRange(faqsFromDb);
        }
    }
}
