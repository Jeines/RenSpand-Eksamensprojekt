using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenSpand_Eksamensprojekt
{
    public class Order
    {
        public int Id { get; set; }

        public List<ServiceItem> ServiceItems { get; set; } = new List<ServiceItem>();

        public decimal TotalPrice { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateDone { get; set; }

        public DateTime? TrashCan { get; set; }

        public Order(int id, List<ServiceItem> serviceItems, decimal totalPrice, DateTime dateStart, DateTime dateDone)
        {
            Id = id;
            ServiceItems = serviceItems;
            TotalPrice = totalPrice;
            DateStart = dateStart;
            DateDone = dateDone;
        }
    }
}