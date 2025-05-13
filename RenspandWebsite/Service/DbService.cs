using Microsoft.EntityFrameworkCore;
using RenspandWebsite.EFDbContext;

namespace RenspandWebsite.Service
{
    public class DbService<T> : IService<T> where T : class
    {

        /// <summary>
        /// Generisk metode til at hente alle objekter af en bestemt type fra databasen
        /// </summary>
        /// <typeparam name="T">Klasse</typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetObjectsAsync()
        {
            using var context = new RenSpandDbContext();
            return await context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Generisk metode til at hente et objekt fra databasen ud fra dets ID
        /// </summary>
        /// <typeparam name="T">Klasse</typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetObjectByIdAsync(int id)
        {
            using var context = new RenSpandDbContext();
            return await context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Generisk metode til at tilføje et nyt objekt til databasen
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
        /// Generisk metode til at opdatere et eksisterende objekt i databasen.
        /// Metoden tjekker om objektet findes i databasen før det opdateres.
        /// Den bruger primærnøglen til at afgøre om objektet findes.
        /// </summary>
        /// <typeparam name="T">Klasse</typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateObjectAsync(T entity)
        {
            using var context = new RenSpandDbContext();
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }


        //TODO: Find ud af hvorfor denne metode ikke virker. Elementerne bliver ikke gemt i databasen.
        /// <summary>
        /// Generisk metode til at gemme en liste af objekter i databasen.
        /// </summary>
        /// <typeparam name="T">Liste af klasser</typeparam>
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
        /// Generisk metode til at slette et objekt fra databasen ud fra objektets id
        /// </summary>
        /// <typeparam name="T">Klasse</typeparam>
        /// <param name="id">Primærnøgle for klassen</param>
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
