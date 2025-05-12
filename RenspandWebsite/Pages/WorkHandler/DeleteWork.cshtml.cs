using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;
using System.Reflection;

namespace RenspandWebsite.Pages.WorkHandler
{
    public class DeleteWorkModel : PageModel
    {
        private IWorkService _workService;
        private ProfileService _profileService;

        public DeleteWorkModel(IWorkService workService, ProfileService profileService)
        {
            _workService = workService;
            _profileService = profileService;
        }

        [BindProperty]
        public RenSpand_Eksamensprojekt.Work Work { get; set; }

        [BindProperty]
        public Profile Profile { get; set; }

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
            //deletedWork = _profileService.DeleteProfile(Work.Id);
            return RedirectToPage("GetAllWork");
        }
    }
}
