using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Models;
using RenspandWebsite.Service;
using RenspandWebsite.Service.WorkServices;

namespace RenspandWebsite.Pages.Admin.AdminProduct
{
    /// <summary>
    /// Denne klasse h�ndterer oprettelsen af et nyt produkt.
    /// </summary>
    public class CreateProductModel : PageModel
    {
        /// <summary>
        /// Produktservicen der bruges til at h�ndtere produktdata.
        /// </summary>
        private WorkService _workService;

        /// <summary>
        /// Initialiserer en ny instans af CreateProductModel-klassen.
        /// </summary>
        /// <param name="workService"></param>
        public CreateProductModel(WorkService workService)
        {
            _workService = workService;
        }

        /// <summary>
        /// Repr�senterer det produkt, der skal oprettes.
        /// </summary>
        [BindProperty]
        public Work Product { get; set; }

        /// <summary>
        /// H�ndterer GET-anmodningen for at oprette et nyt produkt.
        /// </summary>
        /// <returns></returns>
        
        public IActionResult OnGet()
        {
            return Page();
        }

        /// <summary>
        /// H�ndterer POST-anmodningen for at oprette et nyt produkt.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _workService.AddWork(Product);
            return RedirectToPage("GetAllProducts");
        }
    }
}
