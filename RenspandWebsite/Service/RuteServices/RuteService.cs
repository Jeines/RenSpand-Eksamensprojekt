using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service.RuteServices;
using System.Net.Http;
using System.Text.Json;

public class RuteService
{
    private readonly RuteDbService _ruteDbService;
    public RuteService(RuteDbService ruteDbService)
    {
        _ruteDbService = ruteDbService;
    }
    public List<Rute> GetRutes()
    {
        return _ruteDbService.GetObjectsAsync().Result.ToList();
    }
    public Rute GetRute(int id)
    {
        return _ruteDbService.GetObjectByIdAsync(id).Result;
    }
    public void UpdateRute(Rute rute)
    {
        _ruteDbService.UpdateObjectAsync(rute).Wait();
    }
    public void AddRute(Rute rute)
    {
        _ruteDbService.AddObjectAsync(rute).Wait();
    }
    public void DeleteRute(int id)
    {
        _ruteDbService.DeleteObjectAsync(id).Wait();
    }
}
