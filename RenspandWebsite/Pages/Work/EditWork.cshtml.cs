using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Work
{
    public class EditWorkModel : PageModel
    {
        private IWorkService _workService;

        public EditWorkModel(IWorkService workService)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _workService.UpdateWork(Work);
            return RedirectToPage("GetAllWork");
        }
    }
}
