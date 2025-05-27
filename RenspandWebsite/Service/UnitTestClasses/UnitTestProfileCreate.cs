

using Microsoft.AspNetCore.Identity;
using RenspandWebsite.Exceptions;
using RenspandWebsite.Models;
using RenspandWebsite.Service.ProfileServices;
using System.Text.RegularExpressions;

namespace RenspandWebsite.Service.UnitTestClasses
{
    public class UnitTestProfileCreate
    {
        public List<Profile> Profiles { get; set; }


        public UnitTestProfileCreate()
        {
            Profiles = new List<Profile>();
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
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

