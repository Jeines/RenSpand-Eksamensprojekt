using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages
{
    /// <summary>
    /// Sidemodel til FAQ-siden. Henter og viser alle FAQ-elementer fra databasen.
    /// </summary>
    public class FaqSiteModel : PageModel
    {
        private readonly DbService<FAQ> _faqService;

        /// <summary>
        /// Liste over alle FAQ-elementer, der vises på siden.
        /// </summary>
        public List<FAQ> FAQs { get; set; }

        /// <summary>
        /// Initialiserer FaqSiteModel med den givne FAQ-dataservice.
        /// </summary>
        /// <param name="faqService">Service til håndtering af FAQ-data.</param>
        public FaqSiteModel(DbService<FAQ> faqService)
        {
            _faqService = faqService;
        }

        /// <summary>
        /// Henter alle FAQ-elementer asynkront, når siden indlæses.
        /// </summary>
        public async Task OnGetAsync()
        {
            FAQs = (await _faqService.GetObjectsAsync()).ToList();
        }
    }
}
