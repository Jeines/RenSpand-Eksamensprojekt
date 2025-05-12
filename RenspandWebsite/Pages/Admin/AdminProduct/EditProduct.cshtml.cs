using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Admin.AdminProduct
{
    public class EditProductModel : PageModel
    {
        private IWorkService _productService;

        public EditProductModel(IWorkService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Work Product { get; set; }
        public IActionResult OnGet(int id)
        {
            Product = _productService.GetWork(id);
            if (Product == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _productService.UpdateWork(Product);
            return RedirectToPage("GetAllProducts");
        }
    }
}
