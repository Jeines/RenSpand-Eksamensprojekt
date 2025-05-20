namespace RenspandWebsite.Service
{
    public interface IService<T>
    {
        // Henter alle objekter af typen T.
        Task<IEnumerable<T>> GetObjectsAsync();

        // Tilføjer et nyt objekt af typen T.
        Task<T> AddObjectAsync(T obj);

        // Sletter et objekt ud fra dets id.
        Task DeleteObjectAsync(int id);

        // Opdaterer et eksisterende objekt af typen T.
        Task UpdateObjectAsync(T obj);

        // Henter et enkelt objekt ud fra dets id.
        Task<T> GetObjectByIdAsync(int id);

        // Gemmer en liste af objekter af typen T.
        Task SaveObjectsAsync(List<T> objs);
    }
}
