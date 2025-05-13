using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Employee
{
    public class EmployeePageModel : PageModel
    {
        public List<Order> Orders { get; private set; } 

        private readonly OrderService _orderService;

        public EmployeePageModel(OrderService orderServices)
        {
            _orderService = orderServices;
        }

        /// <summary>
        /// Asynchront metode til at hente alle ordrer og gemme dem i Orders-listen.
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            Orders = await _orderService.GetAllOrdersAsync();

            // Uncomment the following lines to see the order details in the console
            //foreach (var order in Orders)
            //{
            //    Console.WriteLine("Order ID: " + order.Id);
            //    Console.WriteLine("Order Status: " + order.AcceptStatus);
            //}
        }

        /// <summary>
        /// Accepts order når et given Id er sendt som parameter og genindlæser siden.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAcceptOrderAsync(int id)
        {
            //Console.WriteLine("AcceptOrder - ID: " + orderId);
            await _orderService.AcceptOrderAsync(id);
            return RedirectToPage();
        }

        /// <summary>
        /// Rejects order når et givet Id er sendt som parameter og genindlæser siden.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostRejectOrderAsync(int id)
        {
            Console.WriteLine("RejectOrder - ID: " + id);
            await _orderService.RejectOrderAsync(id);
            return RedirectToPage();
        }

        /// <summary>
        /// Gemmer en note for ordren med det givne id og genindlæser siden.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostSaveNoteAsync(int id, string note)
        {
            Console.WriteLine("SaveNote - ID: " + id);
            await _orderService.SaveNoteAsync(id, note);
            return RedirectToPage();
        }

        //TODO: Fjernes hvis OnPost metoderne virker
        ///// <summary>
        ///// Accepts order with the given orderId and reloads page
        ///// </summary>
        ///// <param name="orderId"></param>
        ///// <returns></returns>
        //public IActionResult OnPostAcceptOrder(int orderId)
        //{
        //    Console.WriteLine("Acceptorder - ID: " + orderId);
        //    _orderService.AcceptOrder(orderId);
        //    return RedirectToPage();
        //}

        /// <summary>
        /// Rejects order with the given orderId and reloads page
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        //public IActionResult OnPostRejectOrder(int orderId)
        //{
        //    Console.WriteLine("RejectOrder");
        //    _orderService.RejectOrder(orderId);
        //    return RedirectToPage();
        //}

        /// <summary>
        /// Saves a note for the order with the given orderId and reloads page
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        //public IActionResult OnPostSaveNote(int orderId, string note)
        //{
        //    Console.WriteLine("SaveNote");
        //    _orderService.SaveNote(orderId, note);
        //    return RedirectToPage();
        //}
    }
}
