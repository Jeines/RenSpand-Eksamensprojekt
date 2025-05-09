using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RenspandWebsite.Pages.Admin
{
    //[Authorize] // Sikrer at kun autoriserede brugere kommer her
    public class AdminPageModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
