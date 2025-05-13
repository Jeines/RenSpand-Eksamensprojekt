using RenSpand_Eksamensprojekt;


namespace RenspandWebsite.Service.OrderServices
{
    /// <summary>
    /// Service class for handling order-related operations.
    /// </summary>
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
        /// returner alle ordrer
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> GetOrders()
        {
            return _orders;
        }


        /// <summary>
        /// Sætter status til Accept for en order med et givet id og opdaterer ordren i databasen
        /// </summary>
        /// <param name="id"></param>
        public void AcceptOrder(int id) {
            foreach (Order order in _orders)
            {
                if (order.Id == id)
                {
                    // Order bliver sat til accepted
                    order.AcceptStatus = AcceptStatusEnum.Accepted;
                    // updated order in database
                    _orderDbService.UpdateObjectAsync(order);
                    break;
                }
            }
        }

        /// <summary>
        /// Sætter status til Rejects order with the given orderId and updates the order in the database
        /// </summary>
        /// <param name="id"></param>
        public void RejectOrder(int id)
        {
            foreach (Order order in _orders)
            {
                if (order.Id == id)
                {
                    order.AcceptStatus = AcceptStatusEnum.Rejected;
                    // Order bliver sat til rejected
                    _orderDbService.UpdateObjectAsync(order);
                    break;
                }
            }
        }

        /// <summary>
        /// Sætter en note til en order med et givet id og opdaterer ordren i databasen
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="note"></param>
        public void SaveNote(int Id, string note) 
        {
            foreach (Order order in _orders)
            {
                if (order.Id == Id)
                {
                    order.EmployeeNote = note;
                    // Orders Note bliver gemt i database hvis valgt ellers er den empty
                    _orderDbService.UpdateObjectAsync(order);
                    break;
                }
            }
        }
    }
}


