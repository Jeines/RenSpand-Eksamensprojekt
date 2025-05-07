using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    }
}