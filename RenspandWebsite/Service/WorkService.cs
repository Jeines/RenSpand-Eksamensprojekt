using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Service
{
    public class WorkService : IWorkService
    {
        private List<Work> _work;

        private JsonFileService<Work> JsonFileService { get; set; }

        public WorkService(JsonFileService<Work> jsonFileService)
        {
            JsonFileService = jsonFileService;
            _work = JsonFileService.GetJsonObjects().ToList();
        }

        //public WorkService()
        //{
        //    // _items = MockItems.GetMockItems();
        //    //_work = new List<Work>();
        //}
        public void AddWork(Work work)
        {
            _work.Add(work);
            JsonFileService.SaveJsonObjects(_work);
        }

        public Work DeleteWork(int? workId)
        {
            Work workToBeDeleted = null;
            foreach (Work w in _work)
            {
                if (w.Id == workId)
                {
                    workToBeDeleted = w;
                    break;
                }
            }

            if (workToBeDeleted != null)
            {
                _work.Remove(workToBeDeleted);
                JsonFileService.SaveJsonObjects(_work);
            }

            return workToBeDeleted;
        }

        public Work GetWork(int id)
        {
            foreach (Work w in _work)
            {
                if (w.Id == id)
                    return w;
            }

            return null;
        }

        public List<Work> GetWorks()
        {
            return _work;
        }

        public IEnumerable<Work> PriceFilter(int maxPrice, int minPrice = 0)
        {
            List<Work> filterList = new List<Work>();
            foreach (Work w in _work)
            {
                if ((minPrice == 0 && w.Price <= maxPrice) || (maxPrice == 0 && w.Price >= minPrice) || (w.Price >= minPrice && w.Price <= maxPrice))
                {
                    filterList.Add(w);
                }
            }

            return filterList;
        }

        public void UpdateWork(Work work)
        {
            if (work != null)
            {
                foreach (Work w in _work)
                {
                    if (w.Id == work.Id)
                    {
                        w.Name = work.Name;
                        w.Price = work.Price;
                    }
                }
                JsonFileService.SaveJsonObjects(_work);
            }
        }
    }
}
