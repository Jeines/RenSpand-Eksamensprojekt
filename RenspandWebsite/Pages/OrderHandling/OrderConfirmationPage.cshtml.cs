using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RenspandWebsite.Pages.OrderHandling
{
    public class OrderConfirmationPageModel : PageModel
    {
        public void OnGet()
        {
            // Clear the OrderDraft from session after successful order completion
            HttpContext.Session.Remove("OrderDraft");

        }
    }
}
