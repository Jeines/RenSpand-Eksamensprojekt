using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenSpand_Eksamensprojekt
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
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
