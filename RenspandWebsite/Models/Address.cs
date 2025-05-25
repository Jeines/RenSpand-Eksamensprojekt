using System.ComponentModel.DataAnnotations;

namespace RenspandWebsite.Models
{
    /// <summary>
    /// Repræsenterer en adresse med gadenavn, by og postnummer.
    /// </summary>
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string ZipCode { get; set; }

        /// <summary>
        /// Initialiserer en ny instans af Address med angivne værdier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="zipCode"></param>
        public Address(int id, string street, string city, string zipCode)
        {
            Id = id;
            Street = street;
            City = city;
            ZipCode = zipCode;
        }

        public Address() { }
    }
}
