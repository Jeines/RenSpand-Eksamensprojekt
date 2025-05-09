namespace RenspandWebsite.Service;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Pages;

public class CleaningService
{
    private readonly List<Order> _orders;
    private readonly List<Work> _works;
    private JsonFileService<Order> _jsonFileService { get; set; }

    public CleaningService(JsonFileService<Order> jsonFileService)
    {
        _jsonFileService = jsonFileService;
        //_orders = new List<Order>();
        _orders = _jsonFileService.GetJsonObjects().ToList();
        _works = new List<Work>
        {
            new Work(1, "Rengøring", "simpel", 100),
            new Work(2, "Vinduespudsning", "viduer", 200),
            new Work(3, "Havearbejde", "klip græs", 150)
        };
    }

    public void OrderCleaing(User buyer, decimal totalPrice, DateTime dateStart, DateTime dateDone)
    {
        _orders.Add(new Order
        {
            Id = _orders.Count + 1, // Simple ID generation
            Buyer = buyer,
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

        AddressItem addressItem = new AddressItem
        {
            OrderId = 0, // Assuming this is set later
            Address = address
        };

        //Find den valgte Work ud fra Workns id
        Work selectedWork = _works.FirstOrDefault(o => o.Id == work);
        if (selectedWork == null)
        {
            throw new Exception("Work ikke fundet.");
        }

        //Beregn total prisen baseret på mængden af service og serivce prisen.
        decimal totalPrice = selectedWork.Price * workamount;

        //Opret en Order
        Order newOrder = new Order
        {
            Buyer = user,
            AddressItems = new List<AddressItem> { addressItem },
            TotalPrice = totalPrice,
            DateStart = datestart,
            DateDone = datestart.AddDays(8), // Assuming the work is done in one day
            TrashCanEmptyDate = trashcanemptydate,
        };

        //Tilføj den nye ordre til listen
        _orders.Add(newOrder);

        //Gem ordren i JSON filen
        _jsonFileService.SaveJsonObjects(_orders);
    }
}



