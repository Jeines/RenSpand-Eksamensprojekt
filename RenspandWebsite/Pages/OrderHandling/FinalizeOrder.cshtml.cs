using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service.CreateOrderServices;

namespace RenspandWebsite.Pages.OrderHandling
{
    public class FinalizeOrderModel : PageModel
    {
        private CreateOrderService _createOrderService;

        public FinalizeOrderModel(CreateOrderService cleaningService)
        {
            _createOrderService = cleaningService;
        }

        [BindProperty]
        public OrderDraft Draft { get; set; }

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
                var work = _createOrderService.Works.FirstOrDefault(w => w.Id == workId);
                if (work != null)
                {
                    SelectedWorks.Add(work);
                }
            }
        }

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

            // Laver en Ordre med info fra formularen
            await _createOrderService.CreateOrderAsync(
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

            return RedirectToPage("/OrderHandling/OrderConfirmationPage");
        }
    }
}
