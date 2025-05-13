using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RenspandWebsite.Pages.LogIn
{
    public class LogOutPageModel : PageModel
    {
        // Logger Profilen ud og omdiageriger til forsiden
        public async Task<IActionResult> OnGet()
        {
            // Logger Profilen ud
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Omdiageriger til forsiden
            return RedirectToPage("/index");
        }
    }
}
