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
    /// Kun tilgængelig for brugere med rollen "admin".
    /// </summary>
    [Authorize(Roles = "admin")]
    public class AdminFaq : PageModel
    {
        private readonly FaqService _faqService;

        /// <summary>
        /// Initialiserer AdminFaq med en FaqService.
        /// </summary>
        /// <param name="faqService">Service til håndtering af FAQ-data.</param>
        public AdminFaq(FaqService faqService)
        {
            _faqService = faqService;
        }

        /// <summary>
        /// Liste over alle FAQ'er, der vises på siden.
        /// </summary>
        [BindProperty]
        public List<FAQ> Faqs { get; set; } = new();

        /// <summary>
        /// Bindet model til oprettelse af en ny FAQ.
        /// </summary>
        [BindProperty]
        public FAQ NewFaq { get; set; } = new();

        /// <summary>
        /// Henter og viser alle FAQ'er ved indlæsning af siden.
        /// </summary>
        public async Task OnGetAsync()
        {
            var faqs = await _faqService.GetFAQsAsync();
            Faqs = faqs.ToList(); 
        }

        /// <summary>
        /// Tilføjer en ny FAQ, hvis både spørgsmål og svar er udfyldt.
        /// </summary>
        /// <returns>Redirect til siden efter tilføjelse.</returns>
        public async Task<IActionResult> OnPostAdd()
        {
            if (!string.IsNullOrWhiteSpace(NewFaq.Question) && !string.IsNullOrWhiteSpace(NewFaq.Answer))
            {
                await _faqService.AddFAQAsync(NewFaq.Question, NewFaq.Answer);
            }

            return RedirectToPage();
        }

        /// <summary>
        /// Sletter en FAQ baseret på ID.
        /// </summary>
        /// <param name="id">ID på FAQ der skal slettes.</param>
        /// <returns>Redirect til siden efter sletning.</returns>
        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _faqService.DeleteFAQ(id);
            return RedirectToPage();
        }

        /// <summary>
        /// Opdaterer en eksisterende FAQ med nye værdier.
        /// </summary>
        /// <param name="id">ID på FAQ der skal opdateres.</param>
        /// <param name="question">Nyt spørgsmål.</param>
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


