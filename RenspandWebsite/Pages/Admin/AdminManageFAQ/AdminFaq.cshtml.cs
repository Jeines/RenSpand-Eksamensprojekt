using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service.FaqServices;

namespace RenspandWebsite.Pages.Admin.AdminManageFAQ
{
    //[Authorize(Roles = "Admin")]
    public class AdminFaq : PageModel
    {
        private readonly FaqService _faqService;

        public AdminFaq(FaqService faqService)
        {
            _faqService = faqService;
        }

        [BindProperty]
        public List<FAQ> Faqs { get; set; } = new();

        [BindProperty]
        public FAQ NewFaq { get; set; } = new();

        public void OnGet()
        {
            Faqs = _faqService.GetFAQs().ToList();
        }

        public IActionResult OnPostAdd()
        {
            if (!string.IsNullOrWhiteSpace(NewFaq.Question) && !string.IsNullOrWhiteSpace(NewFaq.Answer))
            {
                _faqService.AddFAQ(NewFaq.Question, NewFaq.Answer);
            }

            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            _faqService.DeleteFAQ(id);
            return RedirectToPage();
        }

        public IActionResult OnPostUpdate(int id, string question, string answer)
        {
            _faqService.UpdateFAQ(id, question, answer);
            return RedirectToPage();
        }

    }
}


