using System.Text.Json.Serialization;

namespace RenSpand_Eksamensprojekt
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
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
