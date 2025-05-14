using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;
using RenspandWebsite.Service.WorkServices;

namespace RenspandWebsite.Pages.Admin.AdminProduct
{
    /// <summary>
    /// This class handles the editing of a product.
    /// </summary>
    public class EditProductModel : PageModel
    {
        /// <summary>
        /// The product service used to manage product data.
        /// </summary>
        private WorkService _workService;


        /// <summary>
        /// Initializes a new instance of the EditProductModel class.
        /// </summary>
        /// <param name="productService"></param>
        public EditProductModel(WorkService workService)
        {
            _workService = workService;
        }

        /// <summary>
        /// Represents the product to be edited.
        /// </summary>
        [BindProperty]
        public Work Product { get; set; }

        /// <summary>
        /// Handles the GET request for editing a product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult OnGet(int id)
        {
            Product = _workService.GetWork(id);
            if (Product == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }


        /// <summary>
        /// Handles the POST request for editing a product.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _workService.UpdateWork(Product);
            return RedirectToPage("GetAllProducts");
        }
    }
}
