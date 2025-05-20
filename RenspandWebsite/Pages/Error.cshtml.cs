using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace RenspandWebsite.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        // Id for den aktuelle anmodning
        public string? RequestId { get; set; }

        // Returnerer sand, hvis RequestId er sat
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        // Konstruktør, der modtager en logger
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        // Sætter RequestId ved GET-anmodning
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }

}
