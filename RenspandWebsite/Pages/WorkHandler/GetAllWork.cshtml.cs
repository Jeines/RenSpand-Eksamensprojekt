using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;
using RenspandWebsite.Service.WorkServices;

namespace RenspandWebsite.Pages.WorkHandler
{
    public class GetAllWorkModel : PageModel
    {
        // Service til håndtering af "Work"-data.
        private WorkService _workService;

        // Konstruktør, der modtager en WorkService via dependency injection.
        public GetAllWorkModel(WorkService workService)
        {
            _workService = workService;
        }

        // Liste over alle "Work"-objekter, der vises på siden.
        public List<Work>? Works { get; private set; }

        // Mindstepris for filtrering (bindes fra formular).
        [BindProperty]
        public int MinPrice { get; set; }

        // Højestepris for filtrering (bindes fra formular).
        [BindProperty]
        public int MaxPrice { get; set; }

        // Henter alle "Work"-objekter ved GET-request.
        public void OnGet()
        {
            Works = _workService.GetWorks();
        }

        // Filtrerer "Work"-objekter baseret på prisinterval ved POST-request.
        //public IActionResult OnPostPriceFilter()
        //{
        //    Works = _workService.PriceFilter(MaxPrice, MinPrice).ToList();
        //    return Page();
        //}
    }
}
