using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;
using System.Reflection;

namespace RenspandWebsite.Pages.Work
{
    public class CreateWorkModel : PageModel
    {
        private IWorkService _workService;
        public CreateWorkModel(IWorkService workService)
        {
            _workService = workService;
        }
        [BindProperty]
        public RenSpand_Eksamensprojekt.Work Work { get; set; }

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
            _workService.AddWork(Work);
            return RedirectToPage("GetAllWork");
        }
    }
}
