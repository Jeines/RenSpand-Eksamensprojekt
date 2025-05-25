using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RenspandWebsite.Models
{
    /// <summary>
    /// Repræsenterer en serviceartikel, der forbinder en ordre med et servicearbejde.
    /// </summary>
    public class ServiceItem
    {
        /// <summary>
        /// Propertys til at repræsentere attributterne i ServiceItem.
        /// </summary>
        [Key]
        public int Id { get; set; }
        [Required]
        public int Amount { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [Required]
        public int ServiceWorkId { get; set; }

        [ForeignKey("ServiceWorkId")]
        public Work ServiceWork { get; set; }

        /// <summary>
        /// Initialiserer en ny instans af ServiceItem med angivne værdier.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="orderId"></param>
        /// <param name="serviceWorkId"></param>
        /// <param name="order"></param>
        /// <param name="work"></param>
        public ServiceItem(int amount, int orderId, int serviceWorkId, Order order = null, Work work = null)
        {
            Amount = amount;
            OrderId = orderId;
            ServiceWorkId = serviceWorkId;
            Order = order;
            ServiceWork = work;
        }

        /// <summary>
        /// Default konstruktør til at initialisere en ServiceItem instans.
        /// </summary>
        public ServiceItem() { }
    }
}
