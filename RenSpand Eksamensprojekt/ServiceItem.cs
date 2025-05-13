using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenSpand_Eksamensprojekt
{
    public class ServiceItem
    {
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

        public ServiceItem(int amount, int orderId, int serviceWorkId, Order order = null, Work work = null)
        {
            Amount = amount;
            OrderId = orderId;
            ServiceWorkId = serviceWorkId;
            Order = order;
            ServiceWork = work;
        }

        public ServiceItem() { }
    }
}
