using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenSpand_Eksamensprojekt
{
    public class AddressItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [Required]
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public AddressItem(int orderId, int addressId, Order order = null, Address address = null)
        {
            OrderId = orderId;
            AddressId = addressId;
            Order = order;
            Address = address;
        }

        public AddressItem() { }
    }
}
