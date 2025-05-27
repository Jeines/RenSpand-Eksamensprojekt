using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RenspandWebsite.Models;
using RenspandWebsite.Service;
using RenspandWebsite.Service.WorkServices;

namespace RenspandWebsite.Pages.WorkHandler
{
    public class GetAllWorkModel : PageModel
    {
        // Service til h�ndtering af "Work"-data.
        private WorkService _workService;

        // Konstrukt�r, der modtager en WorkService via dependency injection.
        public GetAllWorkModel(WorkService workService)
        {
            _workService = workService;
        }

        // Liste over alle "Work"-objekter, der vises p� siden.
        public List<Work>? Works { get; private set; }

        // Mindstepris for filtrering (bindes fra formular).
        [BindProperty]
        public int MinPrice { get; set; }

        // H�jestepris for filtrering (bindes fra formular).
        [BindProperty]
        public int MaxPrice { get; set; }



        // Henter alle "Work"-objekter ved GET-request.
        public async Task OnGetAsync()
        {
            Works = (List<Work>)await _workService.GetWorks();
        }
    }
}



