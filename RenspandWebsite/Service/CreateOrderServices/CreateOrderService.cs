namespace RenspandWebsite.Service.CreateOrderServices;
using RenSpand_Eksamensprojekt;

public class CreateOrderService
{
    public List<Work> Works { get; }
    private CreateOrderDbService OrderSystemDbService { get; set; }

    public CreateOrderService(CreateOrderDbService orderSystemDbService)
    {
        OrderSystemDbService = orderSystemDbService;
        Works = OrderSystemDbService.GetAllWorksAsync().Result;
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
    /// <param name="customerNote">Note til køber</param>
    /// <returns></returns>
    public async Task CreateOrderAsync(string name, string email, string phonenumber, string street, string city, string zipcode, List<int[]> workAndAmount, DateTime datestart, DateTime trashcanemptydate, string customerNote)
    {

        // Bruger OrderSystemDbService til at lave en ny ordre i databasen
        await OrderSystemDbService.CreateFullOrderAsync(
            name, email, phonenumber,
            street, city, zipcode,
            workAndAmount,
            datestart, trashcanemptydate,customerNote);
    }
}



