using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.WorkHandler
{
    public class CreateWorkModel : PageModel
    {
        private IWorkService _workService;
        public CreateWorkModel(IWorkService workService)
        {
            _workService = workService;
        }
        [BindProperty]
        public Work Work { get; set; }

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
