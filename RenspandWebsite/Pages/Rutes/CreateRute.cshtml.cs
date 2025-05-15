using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service.OrderServices;

namespace RenspandWebsite.Pages.Rutes
{
    public class RuteCreatorModel : PageModel
    {
        private readonly RuteService _ruteService;
        private readonly OrderService _orderService;
        public RuteCreatorModel(OrderService orderService, RuteService ruteService)
        {
            _orderService = orderService;
            _ruteService = ruteService;
        }


        public void OnGet()
        {
            Console.WriteLine("OnGet");
            // This method is called on GET requests to the page.
            // You can initialize any data you need for the page here.
            foreach (var order in _orderService.GetOrders())
            {
                Console.WriteLine("Orders not null");
                if (order.AcceptStatus == AcceptStatusEnum.Accepted)
                {
                    Console.WriteLine("Order ID: " + order.Id);
                    Console.WriteLine("Order Status: " + order.AcceptStatus);
                }

            }
        }
    }

}
