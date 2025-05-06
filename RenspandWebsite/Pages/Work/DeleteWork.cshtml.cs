using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service;
using System.Reflection;

namespace RenspandWebsite.Pages.Work
{
    public class DeleteWorkModel : PageModel
    {
        private IWorkService _workService;

        public DeleteWorkModel(IWorkService workService)
        {
            _workService = workService;
        }

        [BindProperty]
        public RenSpand_Eksamensprojekt.Work Work { get; set; }

        public IActionResult OnGet(int id)
        {
            Work = _workService.GetWork(id);
            if (Work == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }
        public IActionResult OnPost()
        {
            RenSpand_Eksamensprojekt.Work deletedWork = _workService.DeleteWork(Work.Id);
            if (deletedWork == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return RedirectToPage("GetAllWork");
        }
    }
}
