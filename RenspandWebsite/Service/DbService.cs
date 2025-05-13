using Microsoft.EntityFrameworkCore;
using RenspandWebsite.EFDbContext;

namespace RenspandWebsite.Service
{
    /// <summary>
    /// Generic service klass for håndtering af CRUD-operationer i databasen.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbService<T> : IService<T> where T : class
    {
        private readonly RenSpandDbContext _context;

        /// <summary>
        /// Constructor der initialiserer DbService med en instans af RenSpandDbContext.
        /// </summary>
        /// <param name="context"></param>
        public DbService(RenSpandDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Henter alle objekter af type T fra databasen.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetObjectsAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Henter et objekt af type T fra databasen baseret på dets ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetObjectByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Tilføjer et nyt objekt af type T til databasen.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> AddObjectAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Opdaterer et eksisterende objekt af type T i databasen.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateObjectAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Tilføjer en liste af objekter af type T til databasen.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task SaveObjectsAsync(List<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Sletter et objekt af type T fra databasen baseret på dets ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteObjectAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
