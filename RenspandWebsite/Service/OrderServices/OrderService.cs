using RenSpand_Eksamensprojekt;


namespace RenspandWebsite.Service.OrderServices
{
    /// <summary>
    /// Service class for handling order-related operations.
    /// </summary>
    public class OrderService
    {
        public List<Work> Works { get; }
        private List<Order> _orders; // Corrected type from 'Orders' to 'Order'.  

        private readonly OrderDbService _orderDbService;

        public OrderService(OrderDbService orderDbService)
        {
            _orderDbService = orderDbService;
            _orders = _orderDbService.GetOrdersWithJoinsAsync().Result.ToList();
            Works = _orderDbService.GetAllWorksAsync().Result;
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
            _orders = _orderDbService.GetOrdersWithJoinsAsync().Result.ToList();
        }

        /// <summary>
        /// Sætter status til Rejects order with the given orderId and updates the order in the database
        /// </summary>
        /// <param name="id"></param>
        public void RejectOrder(int id)
        {
            Order order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.AcceptStatus = AcceptStatusEnum.Rejected;
                // Order bliver sat til rejected
                _orderDbService.UpdateObjectAsync(order);
            }
            _orders = _orderDbService.GetOrdersWithJoinsAsync().Result.ToList();
        }

        /// <summary>
        /// Laver en ny ordre i databasen ved brug af OrderSystemDbService.
        /// </summary>
        /// <param name="name">Navn på køber</param>
        /// <param name="email">Email på køber</param>
        /// <param name="phonenumber">TelefonNummer på køber</param>
        /// <param name="street">Vejnavn på køber addresse</param>
        /// <param name="city">By på køber addresse</param>
        /// <param name="zipcode">PostNummer på køber addresse</param>
        /// <param name="workAndAmount">Listen med hvilket og hvor meget arbejde der er bestilt</param>
        /// <param name="datestart">Dato'en ordren er købt fra</param>
        /// <param name="trashcanemptydate">Dato'en køber får tømt skraldespand</param>
        /// <returns></returns>
        public async Task CreateOrderAsync(string name, string email, string phonenumber, string street, string city, string zipcode, List<int[]> workAndAmount, DateTime datestart, DateTime trashcanemptydate, string customerNote)
        {

            // Bruger OrderSystemDbService til at lave en ny ordre i databasen
            await _orderDbService.CreateFullOrderAsync(
                name, email, phonenumber,
                street, city, zipcode,
                workAndAmount,
                datestart, trashcanemptydate, CalculateTotalPrice(workAndAmount), customerNote);
        }


        private decimal CalculateTotalPrice(List<int[]> workAndAmount)
        {
            decimal totalPrice = 0;

            // Antag at du har adgang til en liste af works, her skal du hente priserne på de forskellige works.
            // (F.eks. fra en database eller en hardcoded liste)
            foreach (var entry in workAndAmount)
            {
                int workId = entry[0];
                int amount = entry[1];

                Work work = Works.FirstOrDefault(w => w.Id == workId);
                // Find arbejdet og beregn prisen
                // Her simulerer vi en fast pris per enhed, for demo
                decimal pricePerWork = work.Price;  // Fastsat pris pr. arbejde
                totalPrice += pricePerWork * amount;
            }

            return totalPrice;
        }


        /// <summary>
        /// Sætter en note til en order med et givet id og opdaterer ordren i databasen
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="note"></param>
        //public void SaveNote(int Id, string note) 
        //{
        //    foreach (Order order in _orders)
        //    {
        //        if (order.Id == Id)
        //        {
        //            order.EmployeeNote = note;
        //            // Orders Note bliver gemt i database hvis valgt ellers er den empty
        //            _orderDbService.UpdateObjectAsync(order);
        //            break;
        //        }
        //    }
        //}
    }
}


