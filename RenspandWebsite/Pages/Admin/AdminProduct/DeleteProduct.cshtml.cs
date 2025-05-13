using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Admin.AdminProduct
{
    /// <summary>
    /// This class handles the deletion of a product.
    /// </summary>
    public class DeleteProductModel : PageModel
    {
        /// <summary>
        /// The product service used to manage product data.
        /// </summary>
        private IWorkService _productService;


        /// <summary>
        /// Initializes a new instance of the DeleteProductModel class.
        /// </summary>
        /// <param name="productService"></param>
        public DeleteProductModel(IWorkService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Represents the product to be deleted.
        /// </summary>
        [BindProperty]
        public Work Product { get; set; }


        /// <summary>
        /// Handles the GET request for deleting a product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult OnGet(int id)
        {
            Product = _productService.GetWork(id);
            if (Product == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }

        /// <summary>
        /// Handles the POST request for deleting a product.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            RenSpand_Eksamensprojekt.Work deletedWork = _productService.DeleteWork(Product.Id);
            if (deletedWork == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return RedirectToPage("GetAllProducts");
        }
    }
}
