using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Models;
using RenspandWebsite.Service.OrderServices;
using RenspandWebsite.Service.ProfileServices;
using System.Security.Claims;

namespace RenspandWebsite.Pages.OrderHandling
{
    public class FinalizeOrderModel : PageModel
    {
        private OrderService _orderService;
        private readonly ProfileService _profileService;

        public FinalizeOrderModel(OrderService orderService, ProfileService profileService)
        {
            _orderService = orderService;
            _profileService = profileService;
        }

        [BindProperty]
        public OrderDraft Draft { get; set; }

        /// <summary>
        /// customerNote er en string der indeholder en note fra kunden.
        /// </summary>
        [BindProperty]
        public string CustomerNote { get; set; }

        // WorkAndAmount er en list med id'et på arbejdet og antallet af arbejdet
        [BindProperty]
        public List<int[]> WorkAndAmount { get; set; } = new List<int[]>();

        public List<Work> SelectedWorks { get; set; } = new List<Work>();

        private string Name { get; set; }
        private string Email { get; set; }
        private string PhoneNumber { get; set; }
        private string Street { get; set; }
        private string City { get; set; }
        private string ZipCode { get; set; }
        private DateTime DateStart { get; set; }
        private DateTime TrashCanEmptyDate { get; set; }
        private DateTime DateDone { get; set; }
        private decimal TotalPrice { get; set; }

        /// <summary>
        /// Håndterer GET-anmodningen for at indlæse siden.
        /// </summary>
        public void OnGet()
        {
            // Henter data fra sessionen
            var draftJson = HttpContext.Session.GetString("OrderDraft");

            // Deserialiserer JSON-strengen til OrderDraft objektet
            if (!string.IsNullOrEmpty(draftJson))
            {
                Draft = System.Text.Json.JsonSerializer.Deserialize<OrderDraft>(draftJson);

                // Sætter værdierne fra Draft objektet til de private variabler
                Email = Draft.Email;
                PhoneNumber = Draft.PhoneNumber;
                Street = Draft.Street;
                City = Draft.City;
                ZipCode = Draft.ZipCode;
                DateStart = Draft.DateStart;
                TrashCanEmptyDate = Draft.TrashCanEmptyDate;
                Name = Draft.Name;
            }
            ConvertWorkIdsToWorks();
        }

        /// <summary>
        /// Konverter de valgte workId'er til Work objekter og tilføjer dem til SelectedWorks listen.
        /// </summary>
        public void ConvertWorkIdsToWorks()
        {
            foreach (var workId in Draft.SelectedWorkIds)
            {
                var work = _orderService.Works.FirstOrDefault(w => w.Id == workId);
                if (work != null)
                {
                    SelectedWorks.Add(work);
                }
            }
        }
        /// <summary>
        /// Håndterer POST-anmodningen for at afslutte ordren.
        /// Henter data fra sessionen, deserialiserer OrderDraft-objektet, og opretter en ny ordre med oplysninger fra formularen.
        /// </summary>
        /// <returns>Redirects til bekræftelsessiden for ordren.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            // Henter data fra sessionen
            var draftJson = HttpContext.Session.GetString("OrderDraft");
            // Deserialiserer JSON-strengen til OrderDraft objektet
            if (!string.IsNullOrEmpty(draftJson))
            {
                Draft = System.Text.Json.JsonSerializer.Deserialize<OrderDraft>(draftJson);
            }

            // Henter data fra formularen
            foreach (var item in WorkAndAmount)
            {
                var workId = item[0];
                var amount = item[1];
            }
            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var profile = await _profileService.GetUserDataAsync(int.Parse(userIdClaim));

                // Henter brugerens data
                Name = profile.Name;
                Email = profile.Email;
                PhoneNumber = profile.PhoneNumber;
                Street = Draft.Street;
                City = Draft.City;
                ZipCode = Draft.ZipCode;
                DateStart = Draft.DateStart;
                TrashCanEmptyDate = Draft.TrashCanEmptyDate;
                
                // Opretter ordren som bruger
                await _orderService.CreateOrderIsUser(
                    profile, Draft.Street, Draft.City, Draft.ZipCode, WorkAndAmount, Draft.DateStart, Draft.TrashCanEmptyDate, CustomerNote);
            }
            else
            {
                // Laver en Ordre med info fra formularen
                await _orderService.CreateOrderAsync(
                    Draft.Name,
                    Draft.Email,
                    Draft.PhoneNumber,
                    Draft.Street,
                    Draft.City,
                    Draft.ZipCode,
                    WorkAndAmount,
                    Draft.DateStart,
                    Draft.TrashCanEmptyDate,
                    CustomerNote);
            }
            return RedirectToPage("/OrderHandling/OrderConfirmationPage");
        }
    }
}
