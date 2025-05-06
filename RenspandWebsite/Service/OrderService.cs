using RenSpand_Eksamensprojekt;


namespace RenspandWebsite.Service
{
    public class OrderService
    {
        private List<Order> _orders;  

        private JsonFileService<Order> JsonFileService { get; set; }
        //private DbGenericService<Order> _dbService;
        //TODO: Implement database service for order management (,DbGenericService<Order> dbService)
        public OrderService(JsonFileService<Order> jsonFileService) 
        {
            JsonFileService = jsonFileService;
            // _dbService = dbService;
            _orders = JsonFileService.GetJsonObjects().ToList(); 
            //_orders= _dbService.GetObjectsAsync().Result.ToList();
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
                   where (o.Buyer != null && o.Buyer.Name != null && o.Buyer.PhoneNumber != null) &&
                          (o.Buyer.Name.ToLower().Contains(searchTerm.ToLower()) ||
                           o.Buyer.PhoneNumber.Contains(searchTerm)) ||
                           (o.AddressList != null && o.AddressList.Any(a => a.Street.ToLower().Contains(searchTerm.ToLower()) ||
                                a.City.ToLower().Contains(searchTerm.ToLower()) ||
                                a.ZipCode.ToLower().Contains(searchTerm.ToLower())))
                    select o;
        }

        public IEnumerable<Order> GetOrders()
        {
            return _orders;
        }
    }
    
    

}
    

