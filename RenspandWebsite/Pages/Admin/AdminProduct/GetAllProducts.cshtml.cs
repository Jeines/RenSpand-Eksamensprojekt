using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Models;
using RenspandWebsite.Service;
using RenspandWebsite.Service.WorkServices;

namespace RenspandWebsite.Pages.Admin.AdminProduct
{
    /// <summary>
    /// Denne klasse h�ndterer hentning af alle produkter.
    /// </summary>
    public class GetAllProductsModel : PageModel
    {
        /// <summary>
        /// Produktservicen der bruges til at h�ndtere produktdata.
        /// </summary>
        private WorkService _workService;

        /// <summary>
        /// Initialiserer en ny instans af GetAllProductsModel-klassen.
        /// </summary>
        /// <param name="workService"></param>
        public GetAllProductsModel(WorkService workService)
        {
            _workService = workService;
        }

        /// <summary>
        /// Repr�senterer listen af produkter.
        /// </summary>
        public List<Work>? Products { get; private set; }

        /// <summary>
        /// H�ndterer GET-anmodningen for at hente alle produkter.
        /// </summary>
        [BindProperty]
        public int MinPrice { get; set; }

        /// <summary>
        /// Repr�senterer maksimumsprisen for filtrering af produkter.
        /// </summary>
        [BindProperty]
        public int MaxPrice { get; set; }

        //TODO: Fix sortering af produkter

        /// <summary>
        /// H�ndterer GET-anmodningen for at hente alle produkter.
        /// </summary>
        public void OnGet()
        {
            Products = _workService.GetWorks();
        }

        /// <summary>
        /// H�ndterer POST-anmodningen for at filtrere produkter efter pris.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostPriceFilter()
        {
            Products = _workService.PriceFilter(MaxPrice, MinPrice).ToList();
            return Page();
        }
    }
}
