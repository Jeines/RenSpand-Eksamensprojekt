using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RenspandWebsite.Models
{
    /// <summary>
    /// Repræsenterer en adressepost, der forbinder en ordre med en adresse.
    /// </summary>
    public class AddressItem
    {
        /// <summary>
        /// Propertys til at repræsentere attributterne i AddressItem.
        /// </summary>
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

        /// <summary>
        /// Initialiserer en ny instans af AddressItem med angivne værdier.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="addressId"></param>
        /// <param name="order"></param>
        /// <param name="address"></param>
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
