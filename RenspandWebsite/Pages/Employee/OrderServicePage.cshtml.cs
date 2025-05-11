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
        /// H�ndterer get-foresp�rgsler og filtrerer ordrer via. et s�geterm.
        /// </summary>
        /// <param name="searchTerm">
        /// S�geterm, der bruges til at filtrere ordrer. 
        /// Kan matche k�berens navn, telefonnummer eller adresse (gade, by eller postnummer).
        /// </param>
        /// <remarks>
        /// Denne metode henter alle ordrer fra <see cref="OrderService"/> og filtrerer dem baseret p� s�getermen.
        /// Filtreringen er case-insensitiv og s�ger i f�lgende felter:
        /// - K�berens navn
        /// - K�berens telefonnummer
        /// - Adresselisten (gade, by og postnummer)
        /// Hvis s�getermen er tom eller null, returneres alle ordrer.
        /// </remarks>
        public void OnGet(string searchTerm)
        {
            // Gem s�getermen for brug i visningen
            SearchTerm = searchTerm;

            // Hent alle ordrer fra OrderService
            var allOrders = _orderService.GetOrders();

            /// <summary>  
            /// StringComparison.OrdinalIgnoreCase er en enum-v�rdi, der bruges til at angive  
            /// en case-insensitiv sammenligning af strenge.  
            ///  
            /// - "Ordinal" betyder, at sammenligningen er baseret p� den numeriske v�rdi af hver karakter  
            ///   i strengen, som er dens Unicode-v�rdi.  
            /// - "IgnoreCase" sikrer, at sammenligningen ignorerer forskelle i store og sm� bogstaver.  
            ///  
            /// Hvad er Unicode-v�rdier?  
            /// Unicode er en standard, der tildeler en unik numerisk v�rdi (kaldet en "kodepunkt") til hver karakter  
            /// i forskellige sprog og symboler. For eksempel:  
            /// - 'A' har Unicode-v�rdien 65  
            /// - 'a' har Unicode-v�rdien 97  
            /// - '�' har Unicode-v�rdien 198  
            ///  
            /// Dette er nyttigt, n�r du vil sammenligne strenge uden at bekymre dig om  
            /// forskelle i store og sm� bogstaver.  
            ///  
            /// For mere info vedr�rende "Stringcomparison" tjek link fra microsoft:
            /// 
            /// https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparison  
            /// </summary>

            // Filtrer ordrer baseret p� s�getermen
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
                // Hvis ingen s�geterm er angivet, returneres alle ordrer
                FilteredOrders = allOrders.ToList(); // Konverter IEnumerable til List


            }
        }
    }
}

