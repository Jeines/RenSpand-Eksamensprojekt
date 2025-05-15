using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RenSpand_Eksamensprojekt
{
    public class Order
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int BuyerId { get; set; }

        [ForeignKey("BuyerId")]
        public User Buyer { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public List<AddressItem> AddressItems { get; set; } = new List<AddressItem>();

        public List<ServiceItem> ServiceItems { get; set; } = new List<ServiceItem>();

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public DateTime DateDone { get; set; }
        /// <summary>
        /// Indicates the current status of the order (Pending, Accepted, Rejected, etc.).
        /// </summary>
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AcceptStatusEnum AcceptStatus { get; set; } = AcceptStatusEnum.Pending;

        public DateTime? TrashCanEmptyDate { get; set; }

        //TODO: Tilføj Denne property når resten af implementeringen er klar

        public string? EmployeeNote { get; set; }

        public string? CustomerNote { get; set; }

        public Order(int id, User buyer, List<ServiceItem> serviceItems, decimal totalPrice, DateTime dateStart, DateTime dateDone)
        {
            Id = id;
            Buyer = buyer;
            TotalPrice = totalPrice;
            DateStart = dateStart;
            DateDone = dateDone;
        }

        public Order() { }

        public override string ToString()
        {
            return $"Id: {Id}, Buyer: {Buyer}, ServiceItems: No service items, TotalPrice: {TotalPrice}, DateStart: {DateStart}, DateDone: {DateDone}";
        }
    }
}