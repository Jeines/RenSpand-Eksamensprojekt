using System.ComponentModel.DataAnnotations;

namespace RenSpand_Eksamensprojekt
{
    public class Work
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public Work(int id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
        public Work() { }
    }
}

