using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service.FaqServices;
using System.ComponentModel.DataAnnotations;

namespace RenspandWebsite.Pages.Admin.AdminManageFAQ
{
    /// <summary>
    /// Sidemodel til redigering af en FAQ. Kun tilgængelig for administratorer.
    /// </summary>
    [Authorize(Roles = "admin")]
    public class EditFaqModel : PageModel
    {
        private readonly FaqService _faqService;

        /// <summary>
        /// Initialiserer en ny instans af <see cref="EditFaqModel"/> med den angivne FAQ-service.
        /// </summary>
        /// <param name="faqService">Service til håndtering af FAQ-data.</param>
        public EditFaqModel(FaqService faqService)
        {
            _faqService = faqService;
        }

        /// <summary>
        /// FAQ'ens unikke ID.
        /// </summary>
        [BindProperty]
        public int Id { get; set; }

        /// <summary>
        /// Spørgsmålet i FAQ'en. Skal udfyldes.
        /// </summary>
        [BindProperty, Required]
        public string Question { get; set; } = string.Empty;

        /// <summary>
        /// Svaret i FAQ'en. Skal udfyldes.
        /// </summary>
        [BindProperty, Required]
        public string Answer { get; set; } = string.Empty;

        /// <summary>
        /// Henter FAQ-data baseret på ID og udfylder modellen.
        /// </summary>
        /// <param name="id">ID på FAQ'en der skal redigeres.</param>
        /// <returns>En side med de udfyldte data eller NotFound hvis FAQ ikke findes.</returns>
        public IActionResult OnGet(int id)
        {
            var faq = _faqService.GetFAQById(id);
            if (faq == null) return NotFound();

            Id = faq.Id;
            Question = faq.Question;
            Answer = faq.Answer;

            return Page();
        }

        /// <summary>
        /// Opdaterer FAQ'en med de indtastede værdier hvis modellen er gyldig.
        /// </summary>
        /// <returns>Redirect til FAQ-oversigten eller siden igen ved fejl.</returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _faqService.UpdateFAQ(Id, Question, Answer);
            return RedirectToPage("AdminFaq");
        }
    }
}