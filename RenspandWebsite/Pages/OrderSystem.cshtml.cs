using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service;
using RenSpand_Eksamensprojekt;
using Microsoft.AspNetCore.Mvc.Rendering;




namespace RenspandWebsite.Pages
{
    public class OrderSystemModel : PageModel
    {
        private CleaningService _cleaningService;
        private JsonFileService<Order> _jsonFileService;
        private List<Order> _orders;
        public List<Work> WorkList { get; set; }

        [BindProperty]
        public int Work { get; set; } // Gemmer valgt Service Id

        public List<SelectListItem> WorkSelectList { get; set; }

        public OrderSystemModel(JsonFileService<Order> jsonFileService, CleaningService cleaningService)
        {
            _jsonFileService = jsonFileService;
            _orders = _jsonFileService.GetJsonObjects().ToList();
            _cleaningService = cleaningService;
            WorkList = new List<Work>
            {
                new Work(1,"Rengøring","simpel", 100),
                new Work(2,"Vinduespudsning","viduer", 200),
                new Work(3,"Havearbejde","klip græs", 150)
            };
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
        public int WorkAmount { get; set; }

        [BindProperty]
        public DateTime DateStart { get; set; }

        [BindProperty]
        public DateTime DateDone { get; set; }

        [BindProperty]
        public DateTime TrashCanEmptyDate { get; set; }

        [BindProperty]
        public decimal TotalPrice { get; set; }





        public bool OrderSubmitted { get; set; } = false;
        public List<Order> Orders { get; set; } = new List<Order>();

        public void OnGet()
        {
            Orders = _cleaningService.GetOrders();
            WorkList = new List<Work>
    {

            new (1,"Rengøring","simpel", 100),
            new (2,"Vinduespudsning","viduer", 200),
            new (3,"Havearbejde","klip græs", 150)
    };

            WorkSelectList = WorkList.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"{s.Name} - {s.Description} ({s.Price} kr.)"
            }).ToList();
        }

        public IActionResult OnPost()
        {

            Console.WriteLine("TEST TEST TEST");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("model");
                return Page();
            }
            OrderSubmitted = true;
            _cleaningService.CreateOrder(Name, Email, PhoneNumber, Street, City, ZipCode, Work, WorkAmount, DateStart, TrashCanEmptyDate, TotalPrice);
            return Page();
        }
    }
}