using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace RenSpand_Eksamensprojekt
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RoleEnum Role { get; set; }

        public User(int id, string name, string email, string phoneNumber)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public User() { }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Email: {Email}, PhoneNumber: {PhoneNumber}, Role: {Role}"; ;
        }
    }
}
