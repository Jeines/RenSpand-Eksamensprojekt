using RenSpand_Eksamensprojekt;


namespace RenspandWebsite.Service.OrderServices
{
    public class OrderService
    {
        private readonly List<Order> _orders; // Corrected type from 'Orders' to 'Order'.  

        private JsonFileService<Order> JsonFileService { get; set; }
        private readonly OrderDbService _orderDbService;

        public OrderService(JsonFileService<Order> jsonFileService, OrderDbService orderDbService)
        {
            //JsonFileService = jsonFileService;
            _orderDbService = orderDbService;
            //_orders = JsonFileService.GetJsonObjects().ToList(); 
            //_orders = _orderDbService.GetObjectsAsync().Result.ToList();
            _orders = _orderDbService.GetOrdersWithJoinsAsync().Result.ToList();
        }


        /// <summary>
        /// Søger i listen af ordrer med parametrene navn eller telefonnummer.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public IEnumerable<Order> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return _orders;

            return from o in _orders
                   where o.Buyer != null && o.Buyer.Name != null && o.Buyer.PhoneNumber != null &&
                          (o.Buyer.Name.ToLower().Contains(searchTerm.ToLower()) ||
                           o.Buyer.PhoneNumber.Contains(searchTerm)) ||
                           o.AddressItems != null && o.AddressItems.Any(a => a.Address.Street.ToLower().Contains(searchTerm.ToLower()) ||
                                a.Address.City.ToLower().Contains(searchTerm.ToLower()) ||
                                a.Address.ZipCode.ToLower().Contains(searchTerm.ToLower()))
                   select o;
        }

        /// <summary>
        /// Returns list of orders
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> GetOrders()
        {
            return _orders;
        }


        //TODO: FIX AcceptOrder and RejectOrder and update in the database
        /// <summary>
        /// updates order acceptstatus to accepted
        /// </summary>
        /// <param name="orderid"></param>
        //public void AcceptOrder(int orderid)
        //{
        //    Console.WriteLine("Acceptorder1");
        //    foreach (var order in _orders)
        //    {
        //        if (order.Id == orderid)
        //        {
        //            order.AcceptStatus = AcceptStatusEnum.Accepted;
        //            break;
        //        }
        //    }
        //    JsonFileOrderService.SaveJsonObjects(_orders);
        //}

        ///// <summary>
        ///// removes order from list
        ///// </summary>
        ///// <param name="orderid"></param>
        //public void RejectOrder(int orderid)
        //{
        //    Console.WriteLine("RejectOrder1");
        //    Order orderRemove = null;
        //    foreach (var order in _orders)
        //    {
        //        if (order.Id == orderid)
        //        {
        //            orderRemove = order;
        //            break;
        //        }
        //    }
        //    if (orderRemove != null)
        //    {
        //        _orders.Remove(orderRemove);
        //        JsonFileOrderService.SaveJsonObjects(_orders);
        //    }
        //}
    }
}


