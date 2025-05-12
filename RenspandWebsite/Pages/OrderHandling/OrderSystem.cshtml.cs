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
            // Set default values for the form fields
            DateStart = DateTime.Today;
            TrashCanEmptyDate = DateTime.Today;

            // Populate the work select list
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
                // If the model state is invalid, re-populate the WorkSelectList and return to the page
                WorkList = _createOrderService.Works;
                WorkSelectList = WorkList.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.Name} - ({s.Price} kr.)"
                }).ToList();
                Console.WriteLine("order not created");
                return Page();
            }

            // Package the form data
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

            // Store the draft in the session
            HttpContext.Session.SetString("OrderDraft", System.Text.Json.JsonSerializer.Serialize(draft));

            return RedirectToPage("/OrderHandling/FinalizeOrder");
        }
    }
}