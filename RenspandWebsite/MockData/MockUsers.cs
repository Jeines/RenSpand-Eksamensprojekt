using Microsoft.AspNetCore.Identity;
using RenspandWebsite.EFDbContext;
using RenspandWebsite.Models;
using RenspandWebsite.Service.ProfileServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenspandWebsite.MockData
{
    /// <summary>
    /// MockProfiles klasse til at repræsentere en liste af testbrugere.
    /// </summary>
    public static class MockProfiles
    {
        /// <summary>
        /// Genererer en super admin profil. TIlføjer dem til databasen, hvis de ikke allerede findes.
        /// </summary>
        public static void GetMockProfiles()
        {
            using var db = new RenSpandDbContext();
            if (!db.Profiles.Any(p => p.Username == "superadmin"))
            {
                var passwordHasher = new Microsoft.AspNetCore.Identity.PasswordHasher<string>();

                var adminProfile = new Profile
                {
                    Username = "superadmin",
                    Email = "admin@example.com",
                    Role = RoleEnum.Admin,
                    Password = passwordHasher.HashPassword(null, "superadmin")
                };

                db.Profiles.Add(adminProfile);
                db.SaveChanges();
            }
        }
    }
}
