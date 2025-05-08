using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Employee
{
    public class EmployeePageModel : PageModel
    {
        public List<Order> Orders { get; private set; } 

        private OrderServices _orderServices;

        /// <summary>
        /// loads all orders when page is loaded with a Get request
        /// </summary>
        public void OnGet()
        {
            Orders = _orderServices.GetOrders();
            foreach (var order in Orders)
            {
                Console.WriteLine("Order ID: " + order.Id);
                Console.WriteLine("Order Status: " + order.AcceptStatus);

            }
        }

        public EmployeePageModel(OrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        /// <summary>
        /// Accepts order with the given orderId and reloads page
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IActionResult OnPostAcceptOrder(int orderId)
        {
            Console.WriteLine("Acceptorder - ID: " + orderId);
            _orderServices.AcceptOrder(orderId);
            return RedirectToPage();
        }

        /// <summary>
        /// Rejects order with the given orderId and reloads page
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IActionResult OnPostRejectOrder(int orderId)
        {
            Console.WriteLine("RejectOrder");
            _orderServices.RejectOrder(orderId);
            return RedirectToPage();
        }
    }
}
