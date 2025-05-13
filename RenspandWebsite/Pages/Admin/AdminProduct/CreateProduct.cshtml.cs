using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Admin.AdminProduct
{
    /// <summary>
    /// This class handles the creation of a new product.
    /// </summary>
    public class CreateProductModel : PageModel
    {
        /// <summary>
        /// The product service used to manage product data.
        /// </summary>
        private IWorkService _productService;

        /// <summary>
        /// Initializes a new instance of the CreateProductModel class.
        /// </summary>
        /// <param name="productService"></param>
        public CreateProductModel(IWorkService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Represents the product to be created.
        /// </summary>
        [BindProperty]
        public Work Product { get; set; }

        /// <summary>
        /// Handles the GET request for creating a new product.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGet()
        {
            return Page();
        }

        /// <summary>
        /// Handles the POST request for creating a new product.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _productService.AddWork(Product);
            return RedirectToPage("GetAllProducts");
        }
    }
}
