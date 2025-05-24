using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RenspandWebsite.Models
{
    /// <summary>
    /// Repræsenterer en profil for en bruger, der indeholder oplysninger om brugernavn, adgangskode og adresse.
    /// </summary>
    public class Profile : User
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public int? AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address? Address { get; set; }

        /// <summary>
        /// Initialiserer en ny instans af Profile med angivne værdier.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public Profile(string username, string password)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Initialiserer en ny instans af Profile uden angivne værdier.
        /// </summary>
        public Profile() { }
    }
}
