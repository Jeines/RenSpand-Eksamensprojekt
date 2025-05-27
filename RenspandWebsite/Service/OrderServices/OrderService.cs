using RenspandWebsite.EFDbContext;
using RenspandWebsite.Models;
using System.ComponentModel;


namespace RenspandWebsite.Service.OrderServices
{
    /// <summary>
    /// Serviceklasse til håndtering af ordre-relaterede operationer.
    /// </summary>
    public class OrderService
    {
        // Modify the Works property to include a private setter so it can be assigned internally.
        public List<Work> Works { get; private set; }
        private List<Order> _orders;

        private readonly OrderDbService _orderDbService;

        public OrderService(OrderDbService orderDbService)
        {
            _orderDbService = orderDbService;
        }

        /// <summary>
        /// Initialiserer OrderService ved at hente ordrer og arbejder fra databasen asynkront.
        /// </summary>
        /// <returns></returns>
        public async Task InitAsync()
        {
            // Initialiserer _orders og Works asynkront
            _orders = await _orderDbService.GetOrdersWithJoinsAsync();
            Works = await _orderDbService.GetAllWorksAsync();

        }

        /// <summary>
        /// Opretter en ny instans af OrderService og initialiserer den asynkront med en OrderDbService.
        /// </summary>
        /// <param name="orderDbService"></param>
        /// <returns></returns>
        public static async Task<OrderService> CreateAsync(OrderDbService orderDbService)
        {
            
            var service = new OrderService(orderDbService);
            await service.InitAsync();
            return service;
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

        public async Task<IEnumerable<Order>> GetOrders()
        {
            try
            {
                var orders = await _orderDbService.GetOrdersWithJoinsAsync();
                return orders.ToList();
            }
            catch (Exception ex)
            {
                // TODO: Log fejl her
                Console.WriteLine("Fejl: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
                throw;
            }
        }


        /// <summary>
        /// Sætter status til Accepted for en ordre med et givent id og opdaterer ordren i databasen.
        /// </summary>
        /// <param name="id">Ordre-id</param>
        public async Task AcceptOrder(int id)
        {
            // Hvis _orders er null, hent ordrer asynkront
            if (_orders == null)
            {
                // Hent ordrer fra databasen
                _orders = await _orderDbService.GetOrdersWithJoinsAsync();
            }
            // Find ordren med det givne id
            var order = _orders.FirstOrDefault(o => o.Id == id);
            // Hvis ordren findes, opdater AcceptStatus og gem den i databasen
            if (order != null)
            {
                // Sæt AcceptStatus til Accepted
                order.AcceptStatus = AcceptStatusEnum.Accepted;
                // Opdater ordren i databasen
                await _orderDbService.UpdateObjectAsync(order);
                // Genindlæs ordrer asynkront
                _orders = await _orderDbService.GetOrdersWithJoinsAsync();
            }
        }

        /// <summary>
        /// Sætter status til Rejected for en ordre med et givent id og opdaterer ordren i databasen.
        /// </summary>
        /// <param name="id">Ordre-id</param>
        public async Task RejectOrder(int id)
        {
            // Hvis _orders er null, hent ordrer asynkront
            if (_orders == null)
            {
                // Hent ordrer fra databasen
                _orders = await _orderDbService.GetOrdersWithJoinsAsync();
            }
            // Find ordren med det givne id
            var order = _orders.FirstOrDefault(o => o.Id == id);
            // Hvis ordren findes, opdater AcceptStatus og gem den i databasen
            if (order != null)
            {
                // Sæt AcceptStatus til Rejected
                order.AcceptStatus = AcceptStatusEnum.Rejected;
                // Opdater ordren i databasen
                await _orderDbService.UpdateObjectAsync(order);
                // Genindlæs ordrer asynkront
                _orders = await _orderDbService.GetOrdersWithJoinsAsync();
            }
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
        public async Task  SaveNote(int Id, string note)
        {
            foreach (Order order in _orders)
            {
                if (order.Id == Id)
                {
                    order.EmployeeNote = note;
                    await _orderDbService.UpdateObjectAsync(order);
                    break;
                }
            }
        }

        /// <summary>
        /// ledder efter en ordre med et givet id og ændre IsDone status på ordren, hvis den er Accepted.
        /// Gemmer det i databasen
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>

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


