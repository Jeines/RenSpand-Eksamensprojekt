namespace RenspandWebsite.Models
{
    /// <summary>
    /// Repræsenterer en rute, der indeholder en liste af ordrer.
    /// </summary>
    public class Rute
    {
        /// <summary>
        /// Propertys til at repræsentere attributterne i Rute.
        /// </summary>
        public int Id { get; set; }
        public List<Order> Orders { get; set; }

        /// <summary>
        /// Initialiserer en ny instans af Rute med angivne værdier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ordersList"></param>
        public Rute(int id, List<Order> ordersList)
        {
            Id = id;
            Orders = ordersList;
        }

        /// <summary>
        /// Default konstruktør til at initialisere en Rute instans.
        /// </summary>
        public Rute()
        {
        }

        /// <summary>
        /// Tilføjer en ordre til ruten.
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Order order)
        {
            Orders.Add(order);
        }

        /// <summary>
        /// Fjerner en ordre fra ruten.
        /// </summary>
        /// <param name="order"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void RemoveOrder(Order order)
        {
            if (order != null)
            {
                Orders.Remove(order);
            }
            throw new ArgumentNullException(nameof(order), "Order cannot be null");
        }

        /// <summary>
        /// Fjerner alle ordrer fra ruten.
        /// </summary>
        public void ClearOrders()
        {
            Orders.Clear();
        }

        /// <summary>
        /// Henter listen af ordrer i ruten.
        /// </summary>
        /// <returns></returns>
        public List<Order> GetOrders()
        {
            return Orders;
        }

        /// <summary>
        /// Overrider ToString metoden for at returnere en streng repræsentation af Rute objektet.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            String ordersString = string.Join(", ", Orders);
            return $"Rute ID: {Id}, {ordersString}";
        }

    }
}
