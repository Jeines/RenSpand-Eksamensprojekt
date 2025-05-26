using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;
using System.ComponentModel;


namespace RenspandWebsite.Service.OrderServices
{
    /// <summary>
    /// Serviceklasse til håndtering af ordre-relaterede operationer.
    /// </summary>
    public class OrderService
    {
        public List<Work> Works { get; }
        private List<Order> _orders;

        private readonly OrderDbService _orderDbService;

        public OrderService(OrderDbService orderDbService)
        {
            _orderDbService = orderDbService;
            _orders = _orderDbService.GetOrdersWithJoinsAsync().Result.ToList();
            Works = _orderDbService.GetAllWorksAsync().Result;
        }

        /// <summary>
        /// Søger i listen af ordrer med parametrene navn, telefonnummer eller adresse.
        /// </summary>
        /// <param name="searchTerm">Søgetekst</param>
        /// <returns>Liste af ordrer der matcher søgningen</returns>
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
        /// Returnerer alle ordrer.
        /// </summary>
        /// <returns>Liste af ordrer</returns>
        
        public IEnumerable<Order> GetOrders()
        {
            //return _orders;
            return _orderDbService.GetOrdersWithJoinsAsync().Result.ToList();
        }

        /// <summary>
        /// Sætter status til Accepted for en ordre med et givent id og opdaterer ordren i databasen.
        /// </summary>
        /// <param name="id">Ordre-id</param>
        public void AcceptOrder(int id)
        {
            foreach (Order order in _orders)
            {
                if (order.Id == id)
                {
                    order.AcceptStatus = AcceptStatusEnum.Accepted;
                    _orderDbService.UpdateObjectAsync(order);
                    break;
                }
            }
            _orders = _orderDbService.GetOrdersWithJoinsAsync().Result.ToList();
        }

        /// <summary>
        /// Sætter status til Rejected for en ordre med et givent id og opdaterer ordren i databasen.
        /// </summary>
        /// <param name="id">Ordre-id</param>
        public void RejectOrder(int id)
        {
            Order order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.AcceptStatus = AcceptStatusEnum.Rejected;
                _orderDbService.UpdateObjectAsync(order);
            }
            _orders = _orderDbService.GetOrdersWithJoinsAsync().Result.ToList();
        }

        /// <summary>
        /// Opretter en ny ordre i databasen ved brug af OrderDbService.
        /// </summary>
        /// <param name="name">Navn på køber</param>
        /// <param name="email">Email på køber</param>
        /// <param name="phonenumber">Telefonnummer på køber</param>
        /// <param name="street">Vejnavn på købers adresse</param>
        /// <param name="city">By på købers adresse</param>
        /// <param name="zipcode">Postnummer på købers adresse</param>
        /// <param name="workAndAmount">Liste med hvilke og hvor meget arbejde der er bestilt</param>
        /// <param name="datestart">Dato ordren starter</param>
        /// <param name="trashcanemptydate">Dato for tømning af skraldespand</param>
        /// <param name="customerNote">Kundens note til ordren</param>
        /// <returns>Task</returns>
        public async Task CreateOrderAsync(string name, string email, string phonenumber, string street, string city, string zipcode, List<int[]> workAndAmount, DateTime datestart, DateTime trashcanemptydate, string customerNote)
        {
            await _orderDbService.CreateFullOrderAsync(
                name, email, phonenumber,
                street, city, zipcode,
                workAndAmount,
                datestart, trashcanemptydate, CalculateTotalPrice(workAndAmount), customerNote);
        }

        /// <summary>
        /// Opretter en ordre for en bruger med de angivne oplysninger.
        /// </summary>
        /// <param name="buyer"></param>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="zipcode"></param>
        /// <param name="workAndAmount"></param>
        /// <param name="datestart"></param>
        /// <param name="trashcanemptydate"></param>
        /// <param name="customerNote"></param>
        /// <returns></returns>
        public async Task CreateOrderIsUser(User buyer, string street, string city, string zipcode, List<int[]> workAndAmount, DateTime datestart, DateTime trashcanemptydate, string customerNote)
        {
            await _orderDbService.CreateOrderAsUserAsync(
                buyer, street, city, zipcode, workAndAmount, datestart, trashcanemptydate, CalculateTotalPrice(workAndAmount), customerNote);

        }

        /// <summary>
        /// Udregner den samlede pris for en ordre baseret på valgte arbejder og mængder.
        /// </summary>
        /// <param name="workAndAmount">Liste af arbejder og mængder</param>
        /// <returns>Samlet pris</returns>
        private decimal CalculateTotalPrice(List<int[]> workAndAmount)
        {
            decimal totalPrice = 0;
            foreach (var entry in workAndAmount)
            {
                int workId = entry[0];
                int amount = entry[1];

                Work work = Works.FirstOrDefault(w => w.Id == workId);
                decimal pricePerWork = work.Price;
                totalPrice += pricePerWork * amount;
            }
            return totalPrice;
        }

        

        /// <summary>
        /// Sætter en note til en ordre med et givent id og opdaterer ordren i databasen.
        /// </summary>
        /// <param name="Id">Ordre-id</param>
        /// <param name="note">Medarbejderens note</param>
        //public void SaveNote(int Id, string note) 
        //{
        //    foreach (Order order in _orders)
        //    {
        //        if (order.Id == Id)
        //        {
        //            order.EmployeeNote = note;
        //            _orderDbService.UpdateObjectAsync(order);
        //            break;
        //        }
        //    }
        //}

        // ledder efter en ordre med et givet id og ændre IsDone status på ordren, hvis den er Accepted.
        // Gemmer det i databasen
        public async Task ToggleDoneAsync(int orderId)
        {
            Order order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null && order.AcceptStatus == AcceptStatusEnum.Accepted)
            {
                order.IsDone = !order.IsDone;
                await _orderDbService.UpdateObjectAsync(order);
            }
        }
    }


}


