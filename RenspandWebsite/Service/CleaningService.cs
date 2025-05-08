namespace RenspandWebsite.Service;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Pages;

public class CleaningService
{
    private readonly List<Order> _orders;
    private readonly List<Service> _services;
    private JsonFileService<Order> _jsonFileService { get; set; }

    public CleaningService(JsonFileService<Order> jsonFileService)
    {
        _jsonFileService = jsonFileService;
        //_orders = new List<Order>();
        _orders = _jsonFileService.GetJsonObjects().ToList();
        _services = new List<Service>
        {
            new Service(1, "Rengøring", "simpel", 100),
            new Service(2, "Vinduespudsning", "viduer", 200),
            new Service(3, "Havearbejde", "klip græs", 150)
        };
    }

    public void OrderCleaing(User buyer, List<ServiceItem> serviceItems, decimal totalPrice, DateTime dateStart, DateTime dateDone)
    {
        _orders.Add(new Order
        {
            Id = _orders.Count + 1, // Simple ID generation
            Buyer = buyer,
            ServiceItems = serviceItems,
            TotalPrice = totalPrice,
            DateStart = dateStart,
            DateDone = dateDone,
            

        });

    }
    public List<Order> GetOrders()
    {
        return _orders;
    }

    public void CreateOrder(string name, string email, string phonenumber, string street, string city, string zipcode,int work, int workamount, DateTime datestart, DateTime trashcanemptydate, decimal totalprice)
    {

        //opret User 
        User user = new User
        {
            Name = name,
            Email = email,
            PhoneNumber = phonenumber
        };

        //opret adresse
        Address address = new Address
        {
            Street = street,
            City = city,
            ZipCode = zipcode
        };

        //Find den valgte service ud fra servicens id
        Service selectedService = _services.FirstOrDefault(o => o.Id == work);
        if (selectedService == null)
        {
            throw new Exception("Service ikke fundet.");
        }

        //Beregn total prisen baseret på mængden af service og serivce prisen.
        decimal totalPrice = selectedService.Price * workamount;

        //Opret en Order
        Order newOrder = new Order
        {
            Id = _orders.Count > 0 ? _orders.Max(o => o.Id) + 1 : 1,
            Buyer = user,
            AddressList = new List<Address> { address },
            ServiceItems = new List<ServiceItem>
            {
                new ServiceItem
                {
                    ServiceWork = selectedService,
                    Amount = workamount
                }
            },
            TotalPrice = totalPrice,
            DateStart = datestart,
            DateDone = datestart.AddDays(8), // Assuming the work is done in one day
            TrashCanEmptyDate = trashcanemptydate,
        };

        //Tilføj den nye ordre til listen
        _orders.Add(newOrder);

        //Gem ordren i JSON filen
        _jsonFileService.SaveJsonObjects(_orders);




        ////TODO create user address and order
        //User user = new User
        //{
        //    Name = name,
        //    Email = email,
        //    PhoneNumber = phonenumber
        //};
        //Address address = new Address
        //{
        //    Street = street,
        //    City = city,
        //    ZipCode = zipcode
        //};
        ////TODO convert work to servicework type 
        //ServiceItem serviceItem = new ServiceItem
        //{
        //    ServiceWork = work,
        //    Amount = workamount
        //};

        //Console.WriteLine("det er en test");
        //// Create a new order and add it to the list
        //Order newOrder = new Order
        //{
        //    Id = _orders.Count > 0 ? _orders.Max(o => o.Id) + 1 : 1,
        //    ServiceItems = new List<ServiceItem>(),
        //    Buyer = new User(),
        //    AddressList = new List<Address>(),
        //    TotalPrice = 0,
        //    DateStart = DateTime.Now,
        //    DateDone = DateTime.Now.AddDays(1)
        //};
        //_orders.Add(newOrder);






























    }
    
}



