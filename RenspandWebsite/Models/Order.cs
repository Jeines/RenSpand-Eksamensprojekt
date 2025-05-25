using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RenspandWebsite.Models
{
    /// <summary>
    /// Repræsenterer en ordre, der indeholder oplysninger om køberen, serviceartikler og adresser.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Propertys til at repræsentere attributterne i Order.
        /// </summary>
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

        //Identifies the status of the order. (Pending = 0, Accepted = 1, Rejected = 2)
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AcceptStatusEnum AcceptStatus { get; set; } = AcceptStatusEnum.Pending;

        public DateTime? TrashCanEmptyDate { get; set; }

        [StringLength(100)]
        public string? EmployeeNote { get; set; }
        [StringLength(100)]
        public string? CustomerNote { get; set; }

        public bool IsDone { get; set; } = false;

        /// <summary>
        /// Initialiserer en ny instans af Order med angivne værdier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="buyer"></param>
        /// <param name="serviceItems"></param>
        /// <param name="totalPrice"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateDone"></param>
        public Order(int id, User buyer, List<ServiceItem> serviceItems, decimal totalPrice, DateTime dateStart, DateTime dateDone)
        {
            Id = id;
            Buyer = buyer;
            TotalPrice = totalPrice;
            DateStart = dateStart;
            DateDone = dateDone;
        }

        /// <summary>
        /// Initialiserer en ny instans af Order uden angivne værdier.
        /// </summary>
        public Order() { }

        /// <summary>
        /// Giver en strengrepræsentation af ordren, der indeholder oplysninger om køberen, serviceartikler, totalpris og datoer.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Id: {Id}, Buyer: {Buyer}, ServiceItems: No service items, TotalPrice: {TotalPrice}, DateStart: {DateStart}, DateDone: {DateDone}";
        }
    }
}
