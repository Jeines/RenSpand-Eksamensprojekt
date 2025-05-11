using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using System.Linq;
using System.Collections.Generic;
using RenspandWebsite.Service.OrderServices;

namespace RenspandWebsite.Pages.Employee
{
    public class OrderServicePageModel : PageModel
    {
        private readonly OrderService _orderService;

        public OrderServicePageModel(OrderService orderService)
        {
            _orderService = orderService;
        }

        public List<Order> FilteredOrders { get; set; } = new List<Order>();
        public string SearchTerm { get; set; }

        /// <summary>
        /// Håndterer get-forespørgsler og filtrerer ordrer via. et søgeterm.
        /// </summary>
        /// <param name="searchTerm">
        /// Søgeterm, der bruges til at filtrere ordrer. 
        /// Kan matche køberens navn, telefonnummer eller adresse (gade, by eller postnummer).
        /// </param>
        /// <remarks>
        /// Denne metode henter alle ordrer fra <see cref="OrderService"/> og filtrerer dem baseret på søgetermen.
        /// Filtreringen er case-insensitiv og søger i følgende felter:
        /// - Køberens navn
        /// - Køberens telefonnummer
        /// - Adresselisten (gade, by og postnummer)
        /// Hvis søgetermen er tom eller null, returneres alle ordrer.
        /// </remarks>
        public void OnGet(string searchTerm)
        {
            // Gem søgetermen for brug i visningen
            SearchTerm = searchTerm;

            // Hent alle ordrer fra OrderService
            var allOrders = _orderService.GetOrders();

            /// <summary>  
            /// StringComparison.OrdinalIgnoreCase er en enum-værdi, der bruges til at angive  
            /// en case-insensitiv sammenligning af strenge.  
            ///  
            /// - "Ordinal" betyder, at sammenligningen er baseret på den numeriske værdi af hver karakter  
            ///   i strengen, som er dens Unicode-værdi.  
            /// - "IgnoreCase" sikrer, at sammenligningen ignorerer forskelle i store og små bogstaver.  
            ///  
            /// Hvad er Unicode-værdier?  
            /// Unicode er en standard, der tildeler en unik numerisk værdi (kaldet en "kodepunkt") til hver karakter  
            /// i forskellige sprog og symboler. For eksempel:  
            /// - 'A' har Unicode-værdien 65  
            /// - 'a' har Unicode-værdien 97  
            /// - 'Æ' har Unicode-værdien 198  
            ///  
            /// Dette er nyttigt, når du vil sammenligne strenge uden at bekymre dig om  
            /// forskelle i store og små bogstaver.  
            ///  
            /// For mere info vedrørende "Stringcomparison" tjek link fra microsoft:
            /// 
            /// https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparison  
            /// </summary>

            // Filtrer ordrer baseret på søgetermen
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                FilteredOrders = allOrders
                    .Where(o => (o.Buyer?.Name?.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (o.Buyer?.PhoneNumber?.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (o.AddressItems?.Any(a => a.Address.Street.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                                                         a.Address.City.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                                                         a.Address.ZipCode.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)) ?? false))
                    .ToList();
            }
            else
            {
                // Hvis ingen søgeterm er angivet, returneres alle ordrer
                FilteredOrders = allOrders.ToList(); // Konverter IEnumerable til List


            }
        }
    }
}

