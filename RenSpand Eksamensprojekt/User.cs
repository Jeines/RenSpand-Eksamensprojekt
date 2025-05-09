using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RenSpand_Eksamensprojekt
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
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
