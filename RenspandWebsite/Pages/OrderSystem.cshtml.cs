using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using RenspandWebsite.Service.CreateOrderServices;




namespace RenspandWebsite.Pages
{
    public class OrderSystemModel : PageModel
    {
        private CreateOrderService _cleaningService;
        public List<Work> WorkList { get; set; }

        public List<SelectListItem> WorkSelectList { get; set; }

        public OrderSystemModel(CreateOrderService cleaningService)
        {
            _cleaningService = cleaningService;
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

        [BindProperty, Required(ErrorMessage = "Du skal vælge et produkt.")]
        public int Work { get; set; }

        [BindProperty]
        public DateTime TrashCanEmptyDate { get; set; }

        [BindProperty]
        public decimal TotalPrice { get; set; }

        public void OnGet()
        {
            WorkList = _cleaningService.Works;
            WorkSelectList = WorkList.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"{s.Name} - {s.Description} ({s.Price} kr.)"
            }).ToList();

            //WorkSelectList = WorkList.Select(s => new SelectListItem
            //{
            //    Value = s.Id.ToString(),
            //    Text = $"{s.Name} - {s.Description} ({s.Price} kr.)"
            //}).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If the model state is invalid, re-populate the WorkSelectList and return to the page
                WorkList = _cleaningService.Works;
                WorkSelectList = WorkList.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.Name} - {s.Description} ({s.Price} kr.)"
                }).ToList();
                return Page();
            }

            await _cleaningService.CreateOrderAsync(Name, Email, PhoneNumber, Street, City, ZipCode, Work, WorkAmount, DateStart, TrashCanEmptyDate);

            return RedirectToPage("/OrderHandling/OrderConfirmationPage");
        }
    }
}