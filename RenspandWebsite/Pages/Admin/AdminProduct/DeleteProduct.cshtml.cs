using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;
using RenspandWebsite.Service.WorkServices;

namespace RenspandWebsite.Pages.Admin.AdminProduct
{
    /// <summary>
    /// Denne klasse håndterer sletning af et produkt.
    /// </summary>
    public class DeleteProductModel : PageModel
    {
        /// <summary>
        /// Service til håndtering af produktdata.
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
        /// Repræsenterer det produkt, der skal slettes.
        /// </summary>
        [BindProperty]
        public Work Product { get; set; }

        /// <summary>
        /// Håndterer GET-anmodningen for at slette et produkt.
        /// </summary>
        /// <param name="id">Id på produktet</param>
        /// <returns>Returnerer siden eller redirect hvis produktet ikke findes</returns>
        public IActionResult OnGet(int id)
        {
            Product = _workService.GetWork(id);
            if (Product == null)
                return RedirectToPage("/NotFound"); // NotFound er ikke defineret endnu

            return Page();
        }

        /// <summary>
        /// Håndterer POST-anmodningen for at slette et produkt.
        /// </summary>
        /// <returns>Redirect til oversigt over produkter</returns>
        public IActionResult OnPost()
        {
            _workService.DeleteWork(Product.Id);
            return RedirectToPage("GetAllProducts");
        }
    }
}
