using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;
using System.Reflection;

namespace RenspandWebsite.Pages.Admin
{
    public class CreateProductModel : PageModel
    {
        private IWorkService _productService;
        public CreateProductModel(IWorkService productService)
        {
            _productService = productService;
        }
        [BindProperty]
        public Work Product { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
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
