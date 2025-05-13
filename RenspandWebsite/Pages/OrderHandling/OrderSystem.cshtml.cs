//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using RenSpand_Eksamensprojekt;
//using RenspandWebsite.Service.CreateOrderServices;
//using System.ComponentModel.DataAnnotations;

//TODO: Fjernes vis andet virker


//namespace RenspandWebsite.Pages.OrderHandling
//{
//    public class OrderSystemModel : PageModel
//    {
//        private readonly CreateOrderService _createOrderService;
//        public List<Work> WorkList { get; set; }

//        public List<SelectListItem> WorkSelectList { get; set; }

//        public OrderSystemModel(CreateOrderService cleaningService)
//        {
//            _createOrderService = cleaningService;
//        }


//        [BindProperty]
//        public string Name { get; set; }

//        [BindProperty]
//        public string Email { get; set; }

//        [BindProperty]
//        public string PhoneNumber { get; set; }

//        [BindProperty]
//        public string Street { get; set; }

//        [BindProperty]
//        public string City { get; set; }

//        [BindProperty]
//        public string ZipCode { get; set; }

//        [BindProperty]
//        public List<int> SelectedWorkIds { get; set; }

//        [BindProperty]
//        public DateTime DateStart { get; set; }

//        [BindProperty]
//        public DateTime DateDone { get; set; }

//        [BindProperty]
//        public DateTime TrashCanEmptyDate { get; set; }

//        [BindProperty]
//        public decimal TotalPrice { get; set; }

//        public void OnGet()
//        {
//            DateStart = DateTime.Today;
//            TrashCanEmptyDate = DateTime.Today;

//            WorkList = _createOrderService.Works;
//            WorkSelectList = WorkList.Select(s => new SelectListItem
//            {
//                Value = s.Id.ToString(),
//                Text = $"{s.Name} - ({s.Price} kr.)"
//            }).ToList();
//        }

//        public async Task<IActionResult> OnPostAsync()
//        {
//            if (!ModelState.IsValid)
//            {
//                // If the model state is invalid, re-populate the WorkSelectList and return to the page
//                WorkList = _createOrderService.Works;
//                WorkSelectList = WorkList.Select(s => new SelectListItem
//                {
//                    Value = s.Id.ToString(),
//                    Text = $"{s.Name} - ({s.Price} kr.)"
//                }).ToList();
//                Console.WriteLine("order not created");
//                return Page();
//            }

//            // Package the form data
//            var draft = new OrderDraft
//            {
//                Name = Name,
//                Email = Email,
//                PhoneNumber = PhoneNumber,
//                Street = Street,
//                City = City,
//                ZipCode = ZipCode,
//                SelectedWorkIds = SelectedWorkIds,
//                DateStart = DateStart,
//                TrashCanEmptyDate = TrashCanEmptyDate
//            };

//            // Store the draft in the session
//            HttpContext.Session.SetString("OrderDraft", System.Text.Json.JsonSerializer.Serialize(draft));

//            return RedirectToPage("/OrderHandling/FinalizeOrder");
//        }

//    }
//}


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Threading.Tasks;

namespace RenspandWebsite.Pages.OrderHandling
{
    //TODO : tilføje kommentarer til metoderne
    public class OrderSystemModel : PageModel
    {
        private readonly OrderService _orderService;  // Bruger OrderService i stedet for CreateOrderService
        public List<Work> WorkList { get; set; }

        public List<SelectListItem> WorkSelectList { get; set; }

        // Constructor now takes OrderService
        public OrderSystemModel(OrderService orderService)
        {
            _orderService = orderService;
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

        // Async OnGet to populate the work list
        public async Task OnGetAsync()
        {
            DateStart = DateTime.Today;
            TrashCanEmptyDate = DateTime.Today;

            // Use OrderService to fetch works asynchronously
            WorkList = await _orderService.GetAllWorksAsync();  // Assuming a method like GetAllWorksAsync exists in OrderService
            WorkSelectList = WorkList.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"{s.Name} - ({s.Price} kr.)"
            }).ToList();
        }

        // OnPostAsync to handle form submission
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If the model state is invalid, re-populate the WorkSelectList and return to the page
                WorkList = await _orderService.GetAllWorksAsync();  // Fetch works via OrderService asynchronously
                WorkSelectList = WorkList.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.Name} - ({s.Price} kr.)"
                }).ToList();
                Console.WriteLine("Order not created due to invalid input");
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
            HttpContext.Session.SetString("OrderDraft", JsonSerializer.Serialize(draft));

            // Redirect to the FinalizeOrder page
            return RedirectToPage("/OrderHandling/FinalizeOrder");
        }
    }
}
