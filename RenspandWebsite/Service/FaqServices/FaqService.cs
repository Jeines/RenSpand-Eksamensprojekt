using Microsoft.EntityFrameworkCore;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;

namespace RenspandWebsite.Service.FaqServices
{

    public class FaqService
    {
        public List<FAQ> FAQs { get; }

        private readonly List<FAQ> _faqs;
        private readonly FaqDbService _faqDbService;

        public FaqService(FaqDbService faqDbService)
        {
            _faqDbService = faqDbService;
            _faqs = _faqDbService.GetAllFAQsAsync().Result.ToList();
            FAQs = _faqs;
        }

        /// <summary>
        /// Henter alle FAQs
        /// </summary>
        public IEnumerable<FAQ> GetFAQs()
        {
            return _faqs;
        }

        /// <summary>
        /// Henter en FAQ baseret på ID
        /// </summary>
        public FAQ? GetFAQById(int id)
        {
            return _faqs.FirstOrDefault(f => f.Id == id);
        }

        /// <summary>
        /// Tilføjer en ny FAQ og opdaterer listen
        /// </summary>
        public void AddFAQ(string question, string answer)
        {
            _faqDbService.AddFAQAsync(question, answer).Wait();
            RefreshFAQList();
        }

        /// <summary>
        /// Opdaterer en eksisterende FAQ og opdaterer listen
        /// </summary>
        public void UpdateFAQ(int id, string question, string answer)
        {
            _faqDbService.UpdateFAQAsync(id, question, answer).Wait();
            RefreshFAQList();
        }

        /// <summary>
        /// Sletter en FAQ og opdaterer listen
        /// </summary>
        public void DeleteFAQ(int id)
        {
            _faqDbService.DeleteFAQAsync(id).Wait();
            RefreshFAQList();
        }

        /// <summary>
        /// Opdaterer den interne liste med de nyeste data
        /// </summary>
        private void RefreshFAQList()
        {
            _faqs.Clear();
            _faqs.AddRange(_faqDbService.GetAllFAQsAsync().Result);
        }
    }
}
