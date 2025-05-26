using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RenspandWebsite.Models;
using RenspandWebsite.Service.OrderServices;
using RenspandWebsite.Service.ProfileServices;
using System.Security.Claims;




namespace RenspandWebsite.Pages.OrderHandling
{
    /// <summary>
    /// Model for OrderSystem-siden.
    /// </summary>
    public class OrderSystemModel : PageModel
    {
        /// <summary>
        /// Service til håndtering af ordrer.
        /// </summary>
        private readonly OrderService _orderService;

        /// <summary>
        /// Service til håndtering af brugerprofiler.
        /// </summary>
        private readonly ProfileService _profileService;

        /// <summary>
        /// Liste over arbejder, der kan vælges i bestillingssystemet.
        /// </summary>
        public List<Work> WorkList { get; set; }

        /// <summary>
        /// Liste over arbejder, der kan vælges i dropdown-menuen.
        /// </summary>
        public List<SelectListItem> WorkSelectList { get; set; }

        /// <summary>
        /// Initialiserer en ny instans af OrderSystemModel med de angivne services.
        /// </summary>
        /// <param name="orderService"></param>
        /// <param name="profileService"></param>
        public OrderSystemModel(OrderService orderService, ProfileService profileService)
        {
            _orderService = orderService;
            _profileService = profileService;
        }

        /// <summary>
        /// Repræsenterer de data, der skal indsamles fra brugeren i bestillingssystemet.
        /// </summary>
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string PhoneNumber { get; set; }

        [BindProperty]
        public string Street { get; set; }

        [BindProperty]
        public string City { get; set; }

        [BindProperty]
        public string ZipCode { get; set; }

        [BindProperty]
        public List<int> SelectedWorkIds { get; set; }

        [BindProperty]
        public DateTime DateStart { get; set; }

        [BindProperty]
        public DateTime DateDone { get; set; }

        [BindProperty]
        public DateTime TrashCanEmptyDate { get; set; }

        [BindProperty]
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Sætter værdierne for DateStart og TrashCanEmptyDate til dags dato og henter listen af arbejder fra OrderService.
        /// </summary>
        public void OnGet()
        {
            // Henter Profil data hvis logget ind
            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var profile = _profileService.GetUserData(int.Parse(userIdClaim));
                Name = profile.Name;
                Email = profile.Email;
                PhoneNumber = profile.PhoneNumber;
            }

            // Sætter standardværdier for datoer
            DateStart = DateTime.Today;
            TrashCanEmptyDate = DateTime.Today;

            WorkList = _orderService.Works;
            WorkSelectList = WorkList.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"{s.Name} - ({s.Price} kr.)"
            }).ToList();
        }

        /// <summary>
        /// Håndterer POST-request fra formularen. Validerer model, gemmer OrderDraft i session og viderestiller til FinalizeOrder-siden.
        /// </summary>
        /// <returns>Redirect til FinalizeOrder eller returnerer siden ved fejl</returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Hvis model state er ugyldig, genopbyg WorkSelectList og returner til siden
                WorkList = _orderService.Works;
                WorkSelectList = WorkList.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.Name} - ({s.Price} kr.)"
                }).ToList();
                return Page();
            }

            // Opretter et OrderDraft-objekt med information fra formularen
            var draft = new OrderDraft
            {
                Name = Name,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Street = Street,
                City = City,
                ZipCode = ZipCode,
                SelectedWorkIds = SelectedWorkIds,
                DateStart = DateStart,
                TrashCanEmptyDate = TrashCanEmptyDate
            };

            // Gemmer OrderDraft-objektet i sessionen
            HttpContext.Session.SetString("OrderDraft", System.Text.Json.JsonSerializer.Serialize(draft));

            return RedirectToPage("/OrderHandling/FinalizeOrder");
        }
    }
}