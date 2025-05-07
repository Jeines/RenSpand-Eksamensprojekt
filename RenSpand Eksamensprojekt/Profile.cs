using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenSpand_Eksamensprojekt
{
    public class Profile : User
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public int? AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address? Address { get; set; }
    

    public Profile(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public Profile(){}
    }
}
