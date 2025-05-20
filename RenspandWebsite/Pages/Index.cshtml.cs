using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RenspandWebsite.Pages
{
    /// <summary>
    /// Sidemodel for startsiden.
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// Initialiserer en ny instans af <see cref="IndexModel"/>.
        /// </summary>
        /// <param name="logger">Logger instans til logning.</param>
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Håndterer GET-anmodninger til siden.
        /// </summary>
        public void OnGet()
        {

        }
    }
}
