using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.WorkHandler
{
    public class GetAllWorkModel : PageModel
    {
        private IWorkService _workService;
        public GetAllWorkModel(IWorkService workService)
        {
            _workService = workService;
        }
        public List<Work>? Works { get; private set; }

        [BindProperty]
        public int MinPrice { get; set; }

        [BindProperty]
        public int MaxPrice { get; set; }

        public void OnGet()
        {
            Works = _workService.GetWorks();
        }
        public IActionResult OnPostPriceFilter()
        {
            Works = _workService.PriceFilter(MaxPrice, MinPrice).ToList();
            return Page();
        }
    }
}
