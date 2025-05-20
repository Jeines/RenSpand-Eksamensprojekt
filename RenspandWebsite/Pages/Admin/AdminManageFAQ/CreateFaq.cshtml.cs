using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service.FaqServices;

namespace RenspandWebsite.Pages.Admin.AdminManageFAQ
{
        //[Authorize(Roles = "Admin")]
        public class CreateFaqModel : PageModel
        {
            private readonly FaqService _faqService;

            public CreateFaqModel(FaqService faqService)
            {
                _faqService = faqService;
            }

            [BindProperty]
            public string Question { get; set; } = string.Empty;

            [BindProperty]
            public string Answer { get; set; } = string.Empty;

            public IActionResult OnPost()
            {
                if (!ModelState.IsValid)
                    return Page();

                _faqService.AddFAQ(Question, Answer);
                return RedirectToPage("AdminFaq");
            }
        }
    }