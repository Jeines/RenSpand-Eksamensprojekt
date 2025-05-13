using Microsoft.EntityFrameworkCore;
using RenspandWebsite.EFDbContext;

namespace RenspandWebsite.Service
{
    public class DbService<T> : IService<T> where T : class
    {

        /// <summary>
        /// Generic Get all items of a specific type from the database
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetObjectsAsync()
        {
            using var context = new RenSpandDbContext();
            return await context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Generic Get an item by its ID from the database
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetObjectByIdAsync(int id)
        {
            using var context = new RenSpandDbContext();
            return await context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Generic method to add a new item to the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> AddObjectAsync(T entity)
        {
            using var context = new RenSpandDbContext();
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Generic method to update an existing item in the database.
        /// The methode checks if the item exists in the database before updating it.
        /// It uses the primary to determine if the item exists.
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateObjectAsync(T entity)
        {
            using var context = new RenSpandDbContext();
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }


        //TODO: Find out why this method is not working. the items are not being saved to the database.
        /// <summary>
        /// Generic method to save a list of items to the database.
        /// </summary>
        /// <typeparam name="T">List of classes</typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task SaveObjectsAsync(List<T> entities)
        {
            using var context = new RenSpandDbContext();
            foreach (var entity in entities)
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Generic method to delete an item from the database by the id of the object
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="id">Primary key of the class</param>
        /// <returns></returns>
        public async Task DeleteObjectAsync(int id)
        {
            using var context = new RenSpandDbContext();
            var entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
