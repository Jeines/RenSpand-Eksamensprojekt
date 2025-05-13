namespace RenSpand_Eksamensprojekt
{
    public class Rute
    {
        public int Id { get; set; }
        public List<Order> Orders { get; set; }
        public Rute(int id, List<Order> ordersList)
        {
            Id = id;
            Orders = ordersList;
        }

        public Rute()
        {
        }


        public void AddOrder(Order order)
        {
            Orders.Add(order);
        }

        public void RemoveOrder(Order order)
        {
            if (order != null)
            {
                Orders.Remove(order);
            }
            throw new ArgumentNullException(nameof(order), "Order cannot be null");
        }

        public void ClearOrders()
        {
            Orders.Clear();
        }

        public List<Order> GetOrders()
        {
            return Orders;
        }

        public override string ToString()
        {
            String ordersString = string.Join(", ", Orders);
            return $"Rute ID: {Id}, {ordersString}";
        }

    }
}
