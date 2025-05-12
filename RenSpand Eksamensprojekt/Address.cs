using System.ComponentModel.DataAnnotations;

namespace RenSpand_Eksamensprojekt
{
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
