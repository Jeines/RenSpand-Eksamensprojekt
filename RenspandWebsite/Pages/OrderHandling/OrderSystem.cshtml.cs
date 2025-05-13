using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service.CreateOrderServices;




namespace RenspandWebsite.Pages.OrderHandling
{
    public class OrderSystemModel : PageModel
    {
        private readonly CreateOrderService _createOrderService;
        public List<Work> WorkList { get; set; }

        public List<SelectListItem> WorkSelectList { get; set; }

        public OrderSystemModel(CreateOrderService cleaningService)
        {
            _createOrderService = cleaningService;
        }

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

        public void OnGet()
        {
            // Sætter værdien af DateStart og TrashCanEmptyDate til dagens dato
            DateStart = DateTime.Today;
            TrashCanEmptyDate = DateTime.Today;

            // Henter arbejderne fra CreateOrderService(Databasen)
            WorkList = _createOrderService.Works;
            WorkSelectList = WorkList.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"{s.Name} - ({s.Price} kr.)"
            }).ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Hvis model state er invalid, repopulate WorkSelectList og returner til siden
                WorkList = _createOrderService.Works;
                WorkSelectList = WorkList.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.Name} - ({s.Price} kr.)"
                }).ToList();
                return Page();
            }

            // Laver en OrderDraft objekt med info fra formularen
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

            // Gemmer OrderDraft objektet i sessionen
            HttpContext.Session.SetString("OrderDraft", System.Text.Json.JsonSerializer.Serialize(draft));

            return RedirectToPage("/OrderHandling/FinalizeOrder");
        }
    }
}