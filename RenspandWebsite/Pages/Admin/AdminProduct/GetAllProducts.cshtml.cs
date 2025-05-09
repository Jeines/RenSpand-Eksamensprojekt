using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Admin.AdminProduct
{
    public class GetAllProductsModel : PageModel
    {
        private IWorkService _productService;
        public GetAllProductsModel(IWorkService productService)
        {
            _productService = productService;
        }
        public List<RenSpand_Eksamensprojekt.Work>? Products { get; private set; }

        [BindProperty]
        public int MinPrice { get; set; }

        [BindProperty]
        public int MaxPrice { get; set; }

        public void OnGet()
        {
            Products = _productService.GetWorks();
        }
        public IActionResult OnPostPriceFilter()
        {
            Products = _productService.PriceFilter(MaxPrice, MinPrice).ToList();
            return Page();
        }
    }
}
