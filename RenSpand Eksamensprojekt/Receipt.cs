namespace RenSpand_Eksamensprojekt
{
    public class Receipt
    {
        public int Id { get; set; }

        //public string placeholder { get; set; }

        public string Description { get; set; }

        public Order Order { get; set; }

        public Receipt(int id, string description, Order order)
        {
            Id = id;
            //Picture = picture; 
            Description = description;
            Order = order;

        }
        public Receipt() { }
        public override string ToString()
        {
            return $"{Id} {Description} {Order}";
        }

    }
}
