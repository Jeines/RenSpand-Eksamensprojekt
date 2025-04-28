using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenSpand_Eksamensprojekt
{
    public class Rute
    {
        public int Id { get; set; }
        public List<Order> orders { get; set; }
        public Rute(int id, List<Order> ordersList)
        {
            Id = id;
            orders = ordersList;
        }


        public void AddOrder(Order order)
        {
            orders.Add(order);
        }

        public void RemoveOrder(Order order)
        {
            if (order != null)
            {
                orders.Remove(order);
            }
            throw new ArgumentNullException(nameof(order), "Order cannot be null");
        }

        public void ClearOrders()
        {
            orders.Clear();
        }

        public List<Order> GetOrders()
        {
            return orders;
        }

        public override string ToString()
        {
            String ordersString = string.Join(", ",orders);
            return $"Rute ID: {Id}, {ordersString}";
        }

    }
}
