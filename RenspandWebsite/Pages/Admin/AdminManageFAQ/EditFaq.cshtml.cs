using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service.FaqServices;
using System.ComponentModel.DataAnnotations;

namespace RenspandWebsite.Pages.Admin.AdminManageFAQ
{
    //[Authorize(Roles = "Admin")]
    public class EditFaqModel : PageModel
    {
        private readonly FaqService _faqService;

        public EditFaqModel(FaqService faqService)
        {
            _faqService = faqService;
        }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty, Required]
        public string Question { get; set; } = string.Empty;

        [BindProperty, Required]
        public string Answer { get; set; } = string.Empty;

        public IActionResult OnGet(int id)
        {
            var faq = _faqService.GetFAQById(id);
            if (faq == null) return NotFound();

            Id = faq.Id;
            Question = faq.Question;
            Answer = faq.Answer;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _faqService.UpdateFAQ(Id, Question, Answer);
            return RedirectToPage("AdminFaq");
        }
    }
}