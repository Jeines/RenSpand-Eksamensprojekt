using RenSpand_Eksamensprojekt;


namespace RenspandWebsite.Service
{

    public class OrderService
    {
        private List<Order> _orders; // Corrected type from 'Orders' to 'Order'.  

        private JsonFileService<Order> JsonFileService { get; set; } // Added generic type argument 'Order'.  

        public OrderService(JsonFileService<Order> jsonFileService) // Added generic type argument 'Order'.  
        {
            JsonFileService = jsonFileService;
            _orders = JsonFileService.GetJsonObjects().ToList(); // Removed invalid 'Orders.GetOrders()' call.  
        }

        //TODO: Fix mistakes in this code
        //public IEnumerable<Order> Search(string searchTerm)
        //{
        //    if (string.IsNullOrEmpty(searchTerm)) return _orders;

        //    return from o in _orders
        //           where o.Customer.Name.ToLower().Contains(searchTerm.ToLower()) ||
        //                 o.Customer.Phonenumber.Contains(searchTerm)
        //           select o;
        //}
    }
}
