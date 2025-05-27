using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Models;
using RenspandWebsite.Service;
using RenspandWebsite.Service.WorkServices;

namespace RenspandWebsite.Pages.Admin.AdminProduct
{
    /// <summary>
    /// Denne klasse håndterer redigering af et produkt.
    /// </summary>
    public class EditProductModel : PageModel
    {
        /// <summary>
        /// Service til håndtering af produktdata.
        /// </summary>
        private WorkService _workService;

        /// <summary>
        /// Initialiserer en ny instans af EditProductModel-klassen.
        /// </summary>
        /// <param name="workService">Service til produkter</param>
        public EditProductModel(WorkService workService)
        {
            _workService = workService;
        }

        /// <summary>
        /// Repræsenterer det produkt, der skal redigeres.
        /// </summary>
        [BindProperty]
        public Work Product { get; set; }

        /// <summary>
        /// Håndterer GET-anmodningen for at redigere et produkt.
        /// </summary>
        /// <param name="id">Produktets id</param>
        /// <returns>Viser siden eller redirecter til NotFound</returns>
        public async  Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _workService.GetWork(id);
            if (Product == null)
                return RedirectToPage("/NotFound"); // NotFound er ikke defineret endnu

            return Page();
        }

        /// <summary>
        /// Håndterer POST-anmodningen for at redigere et produkt.
        /// </summary>
        /// <returns>Redirecter til oversigtssiden hvis succes, ellers vises siden igen</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Product == null)
                return RedirectToPage("/NotFound"); // NotFound er ikke defineret endnu
            // Opdater produktet i databasen
            await _workService.UpdateWork(Product);
            return RedirectToPage("GetAllProducts");
        }
    }
}
