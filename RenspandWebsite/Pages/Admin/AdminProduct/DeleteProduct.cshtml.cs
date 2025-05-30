using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Models;
using RenspandWebsite.Service;
using RenspandWebsite.Service.WorkServices;

namespace RenspandWebsite.Pages.Admin.AdminProduct
{
    /// <summary>
    /// Denne klasse h�ndterer sletning af et produkt.
    /// </summary>
    public class DeleteProductModel : PageModel
    {
        /// <summary>
        /// Service til h�ndtering af produktdata.
        /// </summary>
        private WorkService _workService;

        /// <summary>
        /// Initialiserer en ny instans af DeleteProductModel-klassen.
        /// </summary>
        /// <param name="workService">Service til produkter</param>
        public DeleteProductModel(WorkService workService)
        {
            _workService = workService;
        }

        /// <summary>
        /// Repr�senterer det produkt, der skal slettes.
        /// </summary>
        [BindProperty]
        public Work Product { get; set; }

        /// <summary>
        /// H�ndterer GET-anmodningen for at slette et produkt.
        /// </summary>
        /// <param name="id">Id p� produktet</param>
        /// <returns>Returnerer siden eller redirect hvis produktet ikke findes</returns>
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _workService.GetWork(id);
            if (Product == null)
                return RedirectToPage("/NotFound"); // NotFound er ikke defineret endnu

            return Page();
        }

        /// <summary>
        /// H�ndterer POST-anmodningen for at slette et produkt.
        /// </summary>
        /// <returns>Redirect til oversigt over produkter</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (Product == null)
                return RedirectToPage("/NotFound"); // NotFound er ikke defineret endnu
            await _workService.DeleteWork(Product.Id);
            return RedirectToPage("GetAllProducts");
        }
    }
}
