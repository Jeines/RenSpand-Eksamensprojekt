using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service.OrderServices;

namespace RenspandWebsite.Pages.Admin
{
    public class RuteCreatorModel : PageModel
    {
        private readonly OrderService _orderService;
        private static int _ruteCounter = 1;

        public RuteCreatorModel(OrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty]
        public List<Order> AllOrders { get; set; }

        [BindProperty]
        public List<int> SelectedOrderIds { get; set; } = new();

        [BindProperty]
        public Rute GeneratedRute { get; set; }

        public async Task OnGetAsync()
        {
            AllOrders = _orderService.GetOrders()
                .Where(o => o.AcceptStatus == AcceptStatusEnum.Pending)
                .ToList();
        }

        public IActionResult OnPostGenerateRute()
        {
            var selectedOrders = _orderService.GetOrders()
                .Where(o => SelectedOrderIds.Contains(o.Id))
                .Take(5) // Enforce 20 stops max
                .ToList();

            if (selectedOrders.Count == 0)
            {
                ModelState.AddModelError("", "Please select at least one order.");
                return Page();
            }

            GeneratedRute = new Rute(_ruteCounter++, selectedOrders);
            return Page();
        }


        public async Task<IActionResult> OnPostAcceptRuteAsync()
        {
            foreach (var orderId in SelectedOrderIds)
            {
                await _orderService.AcceptOrderAsync(orderId);
            }

            // TODO: Persist Rute if needed in DB (e.g., RuteDbService.CreateRuteAsync(GeneratedRute))

            return RedirectToPage("RuteCreator");
        }
    }
}
