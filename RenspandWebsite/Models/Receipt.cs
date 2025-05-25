namespace RenspandWebsite.Models
{
    /// <summary>
    /// Repræsenterer en kvittering, der indeholder oplysninger om ordren og en beskrivelse.
    /// </summary>
    public class Receipt
    {
        /// <summary>
        /// Propertys til at repræsentere attributterne i Receipt.
        /// </summary>
        public int Id { get; set; }

        //public string placeholder { get; set; }

        public string Description { get; set; }

        public Order Order { get; set; }

        /// <summary>
        /// Initialiserer en ny instans af Receipt med angivne værdier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="order"></param>
        public Receipt(int id, string description, Order order)
        {
            Id = id;
            //Picture = picture; 
            Description = description;
            Order = order;

        }
        /// <summary>
        /// Default konstruktør til at initialisere en Receipt instans.
        /// </summary>
        public Receipt() { }

        /// <summary>
        /// Overrider ToString metoden for at returnere en streng repræsentation af Receipt objektet.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Id} {Description} {Order}";
        }

    }
}
