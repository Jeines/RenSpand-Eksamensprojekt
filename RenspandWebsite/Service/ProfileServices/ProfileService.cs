using Microsoft.AspNetCore.Identity;
using RenspandWebsite.EFDbContext;
using RenspandWebsite.Exceptions;
using RenspandWebsite.MockData;
using RenspandWebsite.Models;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace RenspandWebsite.Service.ProfileServices
{
    public class ProfileService
    {
        public List<Profile> Profiles { get; set; }

        private readonly ProfileDbService _profileDbService;

        public ProfileService(ProfileDbService dbService)
        {
            _profileDbService = dbService;

            // Henter profilerne fra databasen
            Profiles = _profileDbService.GetObjectsAsync().Result.ToList();
            
        }
        /// <summary>
        /// Validerer om passworded til en bruger er korrekt.
        /// </summary>
        /// <param name="id">Id'et på Profilen </param>
        /// <param name="inputPassword">Adgangskode i hash'et format</param>
        /// <returns></returns>
        public bool ValidatePassword(int id, string inputPassword)
        {
            // opdaterer listen af profiler
            Profiles = _profileDbService.GetObjectsAsync().Result.ToList();

            // Finder profilen med det givne id
            Profile profile = Profiles.FirstOrDefault(p => p.Id == id);
            // Hvis profilen findes, validerer vi adgangskoden
            if (profile != null)
            {
                // Opretter en PasswordHasher
                var passwordHasher = new PasswordHasher<string>();
                // Verificerer adgangskoden
                var result = passwordHasher.VerifyHashedPassword(null, profile.Password, inputPassword);
                // Hvis adgangskoden er korrekt, returnerer vi true
                return result == PasswordVerificationResult.Success;
            }
            return false;
        }

        /// <summary>
        /// Henter brugerens data baseret på id.
        /// </summary>
        /// <param name="id">Id'et på Profilen</param>
        /// <returns></returns>
        public Profile GetUserData(int id)
        {
            // opdaterer listen af profiler
            Profiles = _profileDbService.GetObjectsAsync().Result.ToList();

            // Returnerer profilen med det givne id
            return Profiles.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Opdaterer brugerens data i databasen og opdaterer listen af profiler.
        /// </summary>
        /// <param name="id">Id'et på Profilen</param>
        /// <param name="profile"></param>
        public void UpdateUserData(int id, Profile profile)
        {
            // Find the profile with the given ID
            RenspandWebsite.Models.Profile existingProfile = Profiles.FirstOrDefault(p => p.Id == id);
            if (existingProfile != null)
            {
                existingProfile.PhoneNumber = profile.PhoneNumber;
                existingProfile.Name = profile.Name;
                existingProfile.Address = profile.Address;

                _profileDbService.UpdateObjectAsync(existingProfile).Wait();
            }
            // Opdaterer listen af profiler
            Profiles = _profileDbService.GetObjectsAsync().Result.ToList();
        }

        /// <summary>
        /// tilføjer en ny profil til databasen og opdaterer listen af profiler.
        /// </summary>
        /// <param name="profile">Profil objekt</param>
        public void AddProfile(Profile profile)
        {
            // Tjekker om brugeren allerede eksisterer
            if (Profiles.Any(p => p.Username == profile.Username))
            {
                throw new InvalidUsernameException("Brugernavn findes allerede");
            }

            // Tjekker om Brugernavn er over 6 karakterer
            if (profile.Username.Length < 6)
            {
                throw new InvalidUsernameException("Brugernavn skal være længere end 6 karakterer");
            }

            // Tjekker om password er længere end 8 karakterer
            if (profile.Password.Length < 8)
            {
                throw new InvalidPasswordException("Password skal være længere end 8 karakterer");
            }

            // Tjekker om Email er i valid format
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            if (!validateEmailRegex.IsMatch(profile.Email))
            {
                throw new InvalidEmailException("Email er ikke i korrekt format");
            }
            try
            {
                Profiles.Add(profile);
                _profileDbService.AddObjectAsync(profile).Wait();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Opdaterer passwordet for en bruger i databasen og opdaterer listen af profiler.
        /// </summary>
        /// <param name="id">Id'et på Profilen</param>
        /// <param name="newPassword">Nyt Password</param>
        public void UpdatePassWord(int id, string newPassword)
        {
            // Opretter en PasswordHasher
            var passwordHasher = new PasswordHasher<string>();

            // Hash'er det nye password
            var hashedPassword = passwordHasher.HashPassword(null, newPassword);

            // Opdaterer passwordet i databasen
            _profileDbService.UpdatePasswordAsync(id, hashedPassword).Wait();

            // Opdaterer listen af profiler
            Profiles = _profileDbService.GetObjectsAsync().Result.ToList();
        }

        /// <summary>
        /// Henter alle ordrer for en given bruger.
        /// </summary>
        /// <param name="userId">Id'et på Profilen</param>
        /// <returns></returns>
        public async Task<List<Order>> GetUserOrders(int userId)
        {
            return await _profileDbService.GetOrdersByIdAsync(userId);
        }

        /// <summary>
        /// Fjerner en Profil fra Databasen ud fra profilens id.
        /// </summary>
        /// <param name="id">Id'et på Profilen</param>
        public void RemoveProfile(int id)
        {
            // Fjerner Profilen fra databasen
            _profileDbService.DeleteObjectAsync(id).Wait();

            // Opdaterer listen af profiler
            Profiles = _profileDbService.GetObjectsAsync().Result.ToList();
        }
    }
}
