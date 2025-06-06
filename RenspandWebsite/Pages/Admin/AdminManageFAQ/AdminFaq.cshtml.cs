using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using RenspandWebsite.Service.FaqServices;
using RenspandWebsite.Models;
using System.Threading.Tasks;

namespace RenspandWebsite.Pages.Admin.AdminManageFAQ
{
    /// <summary>
    /// Razor PageModel til administration af FAQ (Frequently Asked Questions).
    /// Kun tilg�ngelig for brugere med rollen "admin".
    /// </summary>
    [Authorize(Roles = "admin")]
    public class AdminFaq : PageModel
    {
        private readonly FaqService _faqService;

        /// <summary>
        /// Initialiserer AdminFaq med en FaqService.
        /// </summary>
        /// <param name="faqService">Service til h�ndtering af FAQ-data.</param>
        public AdminFaq(FaqService faqService)
        {
            _faqService = faqService;
        }

        /// <summary>
        /// Liste over alle FAQ'er, der vises p� siden.
        /// </summary>
        [BindProperty]
        public List<FAQ> Faqs { get; set; } = new();

        /// <summary>
        /// Bindet model til oprettelse af en ny FAQ.
        /// </summary>
        [BindProperty]
        public FAQ NewFaq { get; set; } = new();

        /// <summary>
        /// Henter og viser alle FAQ'er ved indl�sning af siden.
        /// </summary>
        public async Task OnGetAsync()
        {
            var faqs = await _faqService.GetFAQsAsync();
            Faqs = faqs.ToList(); 
        }

        /// <summary>
        /// Tilf�jer en ny FAQ, hvis b�de sp�rgsm�l og svar er udfyldt.
        /// </summary>
        /// <returns>Redirect til siden efter tilf�jelse.</returns>
        public async Task<IActionResult> OnPostAdd()
        {
            if (!string.IsNullOrWhiteSpace(NewFaq.Question) && !string.IsNullOrWhiteSpace(NewFaq.Answer))
            {
                await _faqService.AddFAQAsync(NewFaq.Question, NewFaq.Answer);
            }

            return RedirectToPage();
        }

        /// <summary>
        /// Sletter en FAQ baseret p� ID.
        /// </summary>
        /// <param name="id">ID p� FAQ der skal slettes.</param>
        /// <returns>Redirect til siden efter sletning.</returns>
        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _faqService.DeleteFAQ(id);
            return RedirectToPage();
        }

        /// <summary>
        /// Opdaterer en eksisterende FAQ med nye v�rdier.
        /// </summary>
        /// <param name="id">ID p� FAQ der skal opdateres.</param>
        /// <param name="question">Nyt sp�rgsm�l.</param>
        /// <param name="answer">Nyt svar.</param>
        /// <returns>Redirect til siden efter opdatering.</returns>
        public async Task<IActionResult> OnPostUpdate(int id, string question, string answer)
        {
            var existingFaq = await _faqService.GetFAQByIdAsync(id); // Await the Task to get the FAQ object
            if (existingFaq != null)
            {
                existingFaq.Question = question;
                existingFaq.Answer = answer;
                await _faqService.UpdateFAQAsync(existingFaq); // Pass the FAQ object directly
            }
            return RedirectToPage();
        }
    }
}


