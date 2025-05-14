using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;
using RenspandWebsite.Service.WorkServices;

namespace RenspandWebsite.Pages.Admin.AdminProduct
{
    /// <summary>
    /// This class handles the retrieval of all products.
    /// </summary>
    public class GetAllProductsModel : PageModel
    {
        /// <summary>
        /// The product service used to manage product data.
        /// </summary>
        private WorkService _workService;

        /// <summary>
        /// Initializes a new instance of the GetAllProductsModel class.
        /// </summary>
        /// <param name="productService"></param>
        public GetAllProductsModel(WorkService workService)
        {
            _workService = workService;
        }

        /// <summary>
        /// Represents the list of products.
        /// </summary>
        public List<Work>? Products { get; private set; }

        /// <summary>
        /// Handles the GET request for retrieving all products.
        /// </summary>
        [BindProperty]
        public int MinPrice { get; set; }

        /// <summary>
        /// Represents the maximum price for filtering products.
        /// </summary>
        [BindProperty]
        public int MaxPrice { get; set; }

        //TODO: Fix Sorting of products

        /// <summary>
        /// Handles the GET request for retrieving all products.
        /// </summary>
        public void OnGet()
        {
            Products = _workService.GetWorks();
        }
        /// <summary>
        /// Handles the POST request for filtering products by price.
        /// </summary>
        /// <returns></returns>
        //public IActionResult OnPostPriceFilter()
        //{
        //    Products = _productService.PriceFilter(MaxPrice, MinPrice).ToList();
        //    return Page();
        //}
    }
}
