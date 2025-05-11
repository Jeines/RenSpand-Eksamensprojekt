namespace RenspandWebsite.Service
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetObjectsAsync();
        Task<T> AddObjectAsync(T obj);
        Task DeleteObjectAsync(int id);
        Task UpdateObjectAsync(T obj);
        Task<T> GetObjectByIdAsync(int id);
        Task SaveObjectsAsync(List<T> objs);
    }
}
