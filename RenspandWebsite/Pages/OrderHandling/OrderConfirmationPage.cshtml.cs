using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RenspandWebsite.Pages.OrderHandling
{
    public class OrderConfirmationPageModel : PageModel
    {
        public void OnGet()
        {
            // Fjernelse af OrderDraft fra sessionen efter ordren er gennemf�rt
            HttpContext.Session.Remove("OrderDraft");

        }
    }
}
