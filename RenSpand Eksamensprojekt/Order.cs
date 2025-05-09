using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public DateTime DateDone { get; set; }
        /// <summary>
        /// Indicates the current status of the order (Pending, Accepted, Rejected, etc.).
        /// </summary>
        public AcceptStatusEnum AcceptStatus { get; set; } = AcceptStatusEnum.Pending;
        public DateTime? TrashCanEmptyDate { get; set; }
    
        public Order(int id, User buyer, decimal totalPrice, DateTime dateStart, DateTime dateDone)
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
            if (ServiceItems != null && ServiceItems.Count > 0)
            {
                return $"Id: {Id}, Buyer: {Buyer}, ServiceItems: {string.Join(", ", ServiceItems)}, TotalPrice: {TotalPrice}, DateStart: {DateStart}, DateDone: {DateDone}";
            }
            else
            {
                return $"Id: {Id}, Buyer: {Buyer}, ServiceItems: No service items, TotalPrice: {TotalPrice}, DateStart: {DateStart}, DateDone: {DateDone}";
            }
        }

    }
}