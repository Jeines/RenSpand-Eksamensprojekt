using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service.OrderServices;




namespace RenspandWebsite.Pages.OrderHandling
{
    public class OrderSystemModel : PageModel
    {
        private readonly OrderService _orderService;
        public List<Work> WorkList { get; set; }

        public List<SelectListItem> WorkSelectList { get; set; }

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

        /// <summary>
        /// Sætter værdierne for DateStart og TrashCanEmptyDate til dags dato og henter listen af arbejder fra OrderService.
        /// </summary>
        public void OnGet()
        {
            DateStart = DateTime.Today;
            TrashCanEmptyDate = DateTime.Today;

            WorkList = _orderService.Works;
            WorkSelectList = WorkList.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"{s.Name} - ({s.Price} kr.)"
            }).ToList();
        }

        /// <summary>
        /// Håndterer POST-request fra formularen. Validerer model, gemmer OrderDraft i session og viderestiller til FinalizeOrder-siden.
        /// </summary>
        /// <returns>Redirect til FinalizeOrder eller returnerer siden ved fejl</returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Hvis model state er ugyldig, genopbyg WorkSelectList og returner til siden
                WorkList = _orderService.Works;
                WorkSelectList = WorkList.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.Name} - ({s.Price} kr.)"
                }).ToList();
                return Page();
            }

            // Opretter et OrderDraft-objekt med information fra formularen
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

            // Gemmer OrderDraft-objektet i sessionen
            HttpContext.Session.SetString("OrderDraft", System.Text.Json.JsonSerializer.Serialize(draft));

            return RedirectToPage("/OrderHandling/FinalizeOrder");
        }
    }
}