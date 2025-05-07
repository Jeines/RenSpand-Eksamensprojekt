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
        public List<RenSpand_Eksamensprojekt.Service> WorkList { get; set; }

        [BindProperty]
        public int Work { get; set; } // Gemmer valgt Service Id

        public List<SelectListItem> WorkSelectList { get; set; }

        public OrderSystemModel(JsonFileService<Order> jsonFileService, CleaningService cleaningService)
        {
            _jsonFileService = jsonFileService;
            _orders = _jsonFileService.GetJsonObjects().ToList();
            _cleaningService = cleaningService;
            WorkList = new List<RenSpand_Eksamensprojekt.Service>
            {
                new RenSpand_Eksamensprojekt.Service(1,"Rengøring","simpel", 100),
                new RenSpand_Eksamensprojekt.Service(2,"Vinduespudsning","viduer", 200),
                new RenSpand_Eksamensprojekt.Service(3,"Havearbejde","klip græs", 150)
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
            WorkList = new List<RenSpand_Eksamensprojekt.Service>
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
                return Page();
            }

            _cleaningService.CreateOrder(Name, Email, PhoneNumber, Street, City, ZipCode, Work, WorkAmount, DateStart, TrashCanEmptyDate, TotalPrice);
            OrderSubmitted = true;

            return Page();
        }

        public IActionResult OnPostCreateOrder()
        {
            _cleaningService.CreateOrder(Name,Email,PhoneNumber,Street,City,ZipCode,Work,WorkAmount,DateStart,TrashCanEmptyDate,TotalPrice);
            return Page();



        }

        
    }
}








//    public class OrderSystemModel : PageModel
//    {
//        private readonly CleaningService _cleaningService;



//        public bool OrderSubmitted { get; set; } = false;

//        public List<Order> Orders { get; set; } = new List<Order>();


//        private readonly JsonFileService<Order> _jsonFileService;
//        public OrderSystemModel(JsonFileService<Order> jsonFileService)
//        {
//            _jsonFileService = jsonFileService;
//            _cleaningService = new CleaningService();
//        }

//        //public IActionResult OnPost()
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        return Page();
//        //    }

//        //    var order = _jsonFileService.GetJsonObjects().ToList();

//        //    order.Add();

//        //    _jsonFileService.SaveJsonObjects(order);

//        //    OrderSubmitted = true;

//        //    return Page();
//        //}

//        public IActionResult OnPost()
//        {
//            if (ModelState.IsValid)
//            {
//                return Page();
//            }

//            var orders = _jsonFileService.GetJsonObjects().ToList();

//            // Create a new Order object with appropriate values  
//            var newOrder = new Order
//            {
//                Id = orders.Count > 0 ? orders.Max(o => o.Id) + 1 : 1,
//                ServiceItems = new List<ServiceItem>(),
//                Buyer = new User(),
//                AddressList = new List<Address>(),
//                TotalPrice = 0,
//                DateStart = DateTime.Now,
//                DateDone = DateTime.Now.AddDays(1)
//            };

//            orders.Add(newOrder);

//            _jsonFileService.SaveJsonObjects(orders);

//            OrderSubmitted = true;

//            return Page();
//        }


//        public void OnGet()
//        {
//            Orders = _cleaningService.GetOrders();
//        }

//        public IActionResult OnPostOrderCleaning(User buyer, List<ServiceItem> serviceItems, decimal totalPrice, DateTime dateStart, DateTime dateDone)
//        {
//            _cleaningService.OrderCleaing(buyer, serviceItems, totalPrice, dateStart, dateDone);
//            return RedirectToPage();
//        }




//    }
//}
