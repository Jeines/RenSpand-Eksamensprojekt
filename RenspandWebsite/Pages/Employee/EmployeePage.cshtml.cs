using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service.OrderServices;

namespace RenspandWebsite.Pages.Employee
{
    public class EmployeePageModel : PageModel
    {
        public List<Order> Orders { get; private set; }

        private readonly OrderService _orderService;

        /// <summary>
        /// Indlæser alle ordrer, når siden indlæses med en GET-anmodning.
        /// </summary>
        public void OnGet()
        {
            Orders = _orderService.GetOrders().ToList();
            foreach (var order in Orders)
            {
                Console.WriteLine("Order ID: " + order.Id);
                Console.WriteLine("Order Status: " + order.AcceptStatus);
            }
        }

        public EmployeePageModel(OrderService orderServices)
        {
            _orderService = orderServices;
        }
        //TODO: FIX AcceptOrder and RejectOrder and update in the database
        /// <summary>
        /// Accepterer en ordre med det givne orderId og genindlæser siden.
        /// </summary>
        /// <param name="orderId">Id på ordren der skal accepteres</param>
        /// <returns>Redirect til siden</returns>
        public IActionResult OnPostAcceptOrder(int orderId)
        {
            Console.WriteLine("Acceptorder - ID: " + orderId);
            _orderService.AcceptOrder(orderId);
            return RedirectToPage();
        }

        /// <summary>
        /// Afviser en ordre med det givne orderId og genindlæser siden.
        /// </summary>
        /// <param name="orderId">Id på ordren der skal afvises</param>
        /// <returns>Redirect til siden</returns>
        public IActionResult OnPostRejectOrder(int orderId)
        {
            Console.WriteLine("RejectOrder");
            _orderService.RejectOrder(orderId);
            return RedirectToPage();
        }

        //TODO: FIX SaveNote og opdater i databasen
        /// <summary>
        /// Gemmer en note for ordren med det givne orderId og genindlæser siden.
        /// </summary>
        /// <param name="orderId">Id på ordren</param>
        /// <param name="note">Noten der skal gemmes</param>
        /// <returns>Redirect til siden</returns>
        //public IActionResult OnPostSaveNote(int orderId, string note)
        //{
        //    Console.WriteLine("SaveNote");
        //    _orderService.SaveNote(orderId, note);
        //    return RedirectToPage();
        //}
    }
}
