using RenSpand_Eksamensprojekt;

namespace RenspandWebsite.Service
{
    public interface IWorkService
    {
        List<Work> GetWorks();
        void AddWork(Work work);
        void UpdateWork(Work work);
        Work GetWork(int id);
        Work DeleteWork(int? workId);
        IEnumerable<Work> PriceFilter(int maxPrice, int minPrice = 0);
    }
}
