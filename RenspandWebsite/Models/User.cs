using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RenspandWebsite.Models
{
    /// <summary>
    /// Repræsenterer en bruger i systemet, der indeholder oplysninger om navn, e-mail, telefonnummer og rolle.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Propertys til at repræsentere attributterne i User.
        /// </summary>
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Identifies the role of the user. (Admin = 0, Employee = 1, Business = 2, Private = 3, Guest = 4)
        /// </summary>
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RoleEnum Role { get; set; }

        /// <summary>
        /// Initialiserer en ny instans af User med angivne værdier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        public User(int id, string name, string email, string phoneNumber)
        {
            //Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Default konstruktør til at initialisere en User instans.
        /// </summary>
        public User() { }

        /// <summary>
        /// Overrider ToString metoden for at returnere en streng repræsentation af User objektet.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Email: {Email}, PhoneNumber: {PhoneNumber}, Role: {Role}"; ;
        }
    }
}
