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
        /// <param name="userId">Brugerens ID</param>
        /// <returns>Liste af ordrer for brugeren</returns>
        public async Task<List<Order>> GetOrdersByIdAsync(int userId)
        {
            using var context = new RenSpandDbContext();
            return await context.Orders
                .Where(order => order.Buyer.Id == userId)
                .Include(order => order.Buyer)
                .ToListAsync();
        }

        /// <summary>
        /// Validerer om det angivne password matcher en profil i databasen.
        /// </summary>
        /// <param name="password">Password der skal valideres</param>
        /// <returns>Profil hvis password matcher, ellers null</returns>
        public async Task<Profile> PasswordValidation(string password)
        {
            using (var context = new RenSpandDbContext())
            {
                var profile = await context.Profiles
                    .FirstOrDefaultAsync(p => p.Password == password);
                return profile;
            }
        }

        /// <summary>
        /// Opdaterer brugerens password i databasen.
        /// </summary>
        /// <param name="userId">Brugerens ID</param>
        /// <param name="hashedPassword">Det nye (hash'ede) password</param>
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

        /// <summary>
        /// Gemmer ændringer på en profil og returnerer den opdaterede profil.
        /// </summary>
        /// <param name="id">Profilens ID</param>
        /// <returns>Den opdaterede profil</returns>
        public async Task<Profile> SaveProfileChanges(int id)
        {
            using (var context = new RenSpandDbContext())
            {
                var profile = await context.Profiles
                    .Include(p => p.Address)
                    .Include(p => p.PhoneNumber)
                    .Include(p => p.Name)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (profile != null)
                {
                    context.Entry(profile).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                return profile;
            }
        }

        /// <summary>
        /// Henter en profil fra databasen ud fra ID.
        /// </summary>
        /// <param name="id">Profilens ID</param>
        /// <returns>Profil med det angivne ID</returns>
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
