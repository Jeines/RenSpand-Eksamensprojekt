namespace RenspandWebsite.Service;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Pages;

public class CleaningService
{
    public List<Work> Works { get; }
    private JsonFileService<Order> JsonFileService { get; set; }
    private OrderSystemDbService OrderSystemDbService { get; set; }

    public CleaningService(JsonFileService<Order> jsonFileService, OrderSystemDbService orderSystemDbService)
    {
        OrderSystemDbService = orderSystemDbService;
        Works = OrderSystemDbService.GetAllWorksAsync().Result;
    }






    public async Task CreateOrderAsync(string name, string email, string phonenumber, string street, string city, string zipcode, int workId, int workamount, DateTime datestart, DateTime trashcanemptydate)
    {
        // Lookup work from _works or DB
        var selectedWork = Works.FirstOrDefault(w => w.Id == workId) ?? throw new Exception("Work not found");

        // Use the OrderSystemDbService to create the full order
        var order = await OrderSystemDbService.CreateFullOrderAsync(
            name, email, phonenumber,
            street, city, zipcode,
            selectedWork, workamount,
            datestart, trashcanemptydate);
    }
}



