using System.ComponentModel.DataAnnotations;

namespace RenspandWebsite.Models
{
    /// <summary>
    /// Repræsenterer et servicearbejde, der indeholder oplysninger om navn, beskrivelse og pris.
    /// </summary>
    public class Work
    {
        /// <summary>
        /// Propertys til at repræsentere attributterne i Work.
        /// </summary>
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Initialiserer en ny instans af Work med angivne værdier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        public Work(int id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
        /// <summary>
        /// Default konstruktør til at initialisere en Work instans.
        /// </summary>
        public Work() { }

        /// <summary>
        /// Returnerer en strengrepræsentation af arbejdet, der inkluderer id, navn, beskrivelse og pris.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Id: {Id}, Navn: {Name}, Beskrivelse: {Description}, Pris: {Price}";
        }
}
