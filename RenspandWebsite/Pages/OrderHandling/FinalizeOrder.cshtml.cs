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

        // WorkAndAmount will hold pairs of work IDs and amounts (only amounts need to be entered)
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
        // This method is called when the page is 

        public void OnGet()
        {
            // Retrieve the OrderDraft from session
            var draftJson = HttpContext.Session.GetString("OrderDraft");
            if (!string.IsNullOrEmpty(draftJson))
            {
                Draft = System.Text.Json.JsonSerializer.Deserialize<OrderDraft>(draftJson);
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




        // Convert the selected work IDs to a list of works
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
            var draftJson = HttpContext.Session.GetString("OrderDraft");
            if (!string.IsNullOrEmpty(draftJson))
            {
                Draft = System.Text.Json.JsonSerializer.Deserialize<OrderDraft>(draftJson);
            }


            // Process WorkAndAmount data (workId, amount)
            foreach (var item in WorkAndAmount)
            {
                var workId = item[0];  // work ID
                var amount = item[1];   // amount
            }
            Console.WriteLine("Start");
            Console.WriteLine(Draft.Name);
            Console.WriteLine(Draft.Email);
            Console.WriteLine(Draft.PhoneNumber);
            Console.WriteLine(Draft.Street);
            Console.WriteLine(Draft.City);
            Console.WriteLine(Draft.ZipCode);
            Console.WriteLine(Draft.DateStart);
            Console.WriteLine(Draft.TrashCanEmptyDate);
            Console.WriteLine(WorkAndAmount);
            Console.WriteLine("End");



            // Create the order using the OrderSystemDbService
            await _createOrderService.CreateOrderAsync(
                Draft.Name,
                Draft.Email,
                Draft.PhoneNumber,
                Draft.Street,
                Draft.City,
                Draft.ZipCode,
                WorkAndAmount,
                Draft.DateStart,
                Draft.TrashCanEmptyDate);

            return RedirectToPage("/OrderHandling/OrderConfirmationPage");
        }


    }
}
