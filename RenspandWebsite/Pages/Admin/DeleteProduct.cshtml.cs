using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;
using System.Reflection;

namespace RenspandWebsite.Pages.Admin
{
    public class DeleteProductModel : PageModel
    {
        private IWorkService _productService;

        public DeleteProductModel(IWorkService productService)
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
            Work deletedWork = _productService.DeleteWork(Product.Id);
            if (deletedWork == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return RedirectToPage("GetAllProducts");
        }
    }
}
