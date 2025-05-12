namespace RenspandWebsite.Service.CreateOrderServices;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Pages;

public class CreateOrderService
{
    public List<Work> Works { get; }
    private JsonFileService<Order> JsonFileService { get; set; }
    private CreateOrderDbService OrderSystemDbService { get; set; }

    public CreateOrderService(JsonFileService<Order> jsonFileService, CreateOrderDbService orderSystemDbService)
    {
        OrderSystemDbService = orderSystemDbService;
        Works = OrderSystemDbService.GetAllWorksAsync().Result;
    }

    public async Task CreateOrderAsync(string name, string email, string phonenumber, string street, string city, string zipcode, List<int[]> workAndAmount, DateTime datestart, DateTime trashcanemptydate)
    {

        // Use the OrderSystemDbService to create the full order
        var order = await OrderSystemDbService.CreateFullOrderAsync(
            name, email, phonenumber,
            street, city, zipcode,
            workAndAmount,
            datestart, trashcanemptydate);
    }
}



