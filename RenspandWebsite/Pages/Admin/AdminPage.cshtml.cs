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
            if (User.IsInRole("Admin"))
            {
                return Page(); // fortsæt som normalt hvis admin
            }
            //return RedirectToPage("/Index"); // redirect til forsiden  

            return Page();
        }
    }
}
