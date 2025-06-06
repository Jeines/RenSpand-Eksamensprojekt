using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service.FaqServices;
using System.Threading.Tasks;

namespace RenspandWebsite.Pages.Admin.AdminManageFAQ
{
    /// <summary>
    /// Sidemodel til oprettelse af en ny FAQ. Kun tilgængelig for brugere med rollen 'admin'.
    /// </summary>
    [Authorize(Roles = "admin")]
    public class CreateFaqModel : PageModel
    {
        private readonly FaqService _faqService;

        /// <summary>
        /// Initialiserer en ny instans af <see cref="CreateFaqModel"/> med den angivne FaqService.
        /// </summary>
        /// <param name="faqService">Service til håndtering af FAQ-data.</param>
        public CreateFaqModel(FaqService faqService)
        {
            _faqService = faqService;
        }

        /// <summary>
        /// Spørgsmålet til den nye FAQ. Bindes fra formularen.
        /// </summary>
        [BindProperty]
        public string Question { get; set; } = string.Empty;

        /// <summary>
        /// Svaret til den nye FAQ. Bindes fra formularen.
        /// </summary>
        [BindProperty]
        public string Answer { get; set; } = string.Empty;

        /// <summary>
        /// Håndterer POST-anmodninger for at oprette en ny FAQ.
        /// </summary>
        /// <returns>Redirect til FAQ-oversigten ved succes, ellers vises siden igen.</returns>
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            await _faqService.AddFAQAsync(Question, Answer);
            return RedirectToPage("AdminFaq");
        }
    }
    }