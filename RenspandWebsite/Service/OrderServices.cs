using RenSpand_Eksamensprojekt;
namespace RenspandWebsite.Service
{
    public class OrderServices
    {
        private JsonFileService<Order> JsonFileOrderService { get; set; }

        private List<Order> _Orders;

        /// <summary>
        /// Constructor for OrderServices
        /// </summary>
        /// <param name="jsonFileOrderService"></param>
        public OrderServices(JsonFileService<Order> jsonFileOrderService)
        {
            JsonFileOrderService = jsonFileOrderService;
            _Orders = JsonFileOrderService.GetJsonObjects().ToList();
        }

        /// <summary>
        /// Returns list of orders
        /// </summary>
        /// <returns></returns>
        public List<Order> GetOrders()
        {
            return _Orders;
        }

        /// <summary>
        /// updates order acceptstatus to accepted
        /// </summary>
        /// <param name="orderid"></param>
        public void AcceptOrder(int orderid)
        {
            Console.WriteLine("Acceptorder1");
            foreach (var order in _Orders)
            {
                if (order.Id == orderid)
                {
                    order.AcceptStatus = EnumClass.AcceptStatus.Accepted;
                    break;
                }
            }
            JsonFileOrderService.SaveJsonObjects(_Orders);
        }
        /// <summary>
        /// removes order from list
        /// </summary>
        /// <param name="orderid"></param>
        public void RejectOrder(int orderid)
        {
            Console.WriteLine("RejectOrder1");
            Order orderRemove = null;
            foreach (var order in _Orders)
            {
                if (order.Id == orderid)
                {
                    orderRemove = order;
                    break;
                }
            }
            if (orderRemove != null)
            {
                _Orders.Remove(orderRemove);
                JsonFileOrderService.SaveJsonObjects(_Orders);
            }
        }

        /// <summary>
        /// Saves a note to the order with the given orderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="note"></param>
        public void SaveNote(int orderId, string note)
        {
            foreach (var order in _Orders)
            {
                if (order.Id == orderId)
                {
                    order.EmployeeNote = note;
                    break;
                }
            }
            JsonFileOrderService.SaveJsonObjects(_Orders);
        }
    }
}