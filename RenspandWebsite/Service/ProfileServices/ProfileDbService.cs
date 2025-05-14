using Microsoft.EntityFrameworkCore;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;

namespace RenspandWebsite.Service.ProfileServices
{
    public class ProfileDbService : DbService<Profile>
    {
        /// <summary>
        /// Henter alle ordrer fra databasen for en given bruger.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Order>> GetOrdersByIdAsync(int userId)
        {
            using var context = new RenSpandDbContext();
            return await context.Orders
            .Where(order => order.Buyer.Id == userId)
            .Include(order => order.Buyer)
            .ToListAsync();
        }

        /// <summary>
        /// Tjekker om passwordet er korrekt.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Profile> PasswordValidation(string password)
        {
            using (var context = new RenSpandDbContext())
            {
                var profile = await context.Profiles
                    .Include(p => p.Password)
                    .FirstOrDefaultAsync(p => p.Password == password);
                return profile;
            }
        }

        public async Task UpdatePasswordAsync(int userId, string hashedPassword)
        {
            using (var context = new RenSpandDbContext())
            {
                var profile = context.Profiles.FirstOrDefault(p => p.Id == userId);
                if (profile != null)
                {
                    profile.Password = hashedPassword;
                    context.Profiles.Update(profile);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<Profile> SaveProfileChanges(int id)
        {
            using (var context = new RenSpandDbContext())
            {
                var profile = await context.Profiles
                    .Include(p => p.Address)
                    .Include(p => p.Password)
                    .Include(p => p.PhoneNumber)
                    .Include(p => p.Name)
                    //.Include(p => p.Address.Street)
                    //.Include(p => p.Address.ZipCode) // til fremtiden hvis Anders vil udvide hans salgs areal.
                    //.Include(p => p.Address.City) // til fremtiden hvis Anders vil udvide hans salgs areal.
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (profile != null)
                {
                    context.Entry(profile).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                return profile;
            }
        }

        public async Task<Profile> GetProfileByIdAsync(int id)
        {
            using (var context = new RenSpandDbContext())
            {
                return await context.Profiles
                    .Include(p => p.Address)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
        }
    }
}
