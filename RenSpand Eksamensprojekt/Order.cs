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

        public User Buyer { get; set; }

        public List<Address> AddressList { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateDone { get; set; }

        public DateTime? TrashCanEmptyDate { get; set; }
    
        public Order(int id, User buyer, List<ServiceItem> serviceItems, decimal totalPrice, DateTime dateStart, DateTime dateDone)
        {
            Id = id;
            Buyer = buyer;
            ServiceItems = serviceItems;
            TotalPrice = totalPrice;
            DateStart = dateStart;
            DateDone = dateDone;
        }

        public Order() { }

        public override string ToString()
        {
            if (ServiceItems != null && ServiceItems.Count > 0)
            {
                return $"Id: {Id}, Buyer: {Buyer}, ServiceItems: {string.Join(", ", ServiceItems)}, TotalPrice: {TotalPrice}, DateStart: {DateStart}, DateDone: {DateDone}";
            }
            else
            {
                return $"Id: {Id}, Buyer: {Buyer}, ServiceItems: No service items, TotalPrice: {TotalPrice}, DateStart: {DateStart}, DateDone: {DateDone}";
            }
        }

    }
}