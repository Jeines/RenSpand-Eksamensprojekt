using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages
{
    public class FaqSiteModel : PageModel
    {

        private readonly DbService<FAQ> _faqService;
        public List<FAQ> FAQs { get; set; }

        public FaqSiteModel(DbService<FAQ> faqService)
        {
            _faqService = faqService;
        }

        public async Task OnGetAsync()
        {
            FAQs = (await _faqService.GetObjectsAsync()).ToList();
        }
    }
}
