using RenSpand_Eksamensprojekt;

namespace RenspandWebsite.Service.WorkServices
{
    public class WorkService
    {
        private readonly WorkDbService _workDbService;

        public WorkService(WorkDbService workDbService)
        {
            _workDbService = workDbService;
        }

        public List<Work> GetWorks()
        {
            return _workDbService.GetObjectsAsync().Result.ToList();
        }

        public Work GetWork(int id)
        {
            return _workDbService.GetObjectByIdAsync(id).Result;
        }

        public void UpdateWork(Work work)
        {
            _workDbService.UpdateObjectAsync(work).Wait();
        }

        public void AddWork(Work work)
        {
            _workDbService.AddObjectAsync(work).Wait();
        }

        public void DeleteWork(int id)
        {
            _workDbService.DeleteObjectAsync(id).Wait();
        }


    }
}
