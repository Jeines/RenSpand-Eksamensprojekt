using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenSpand_Eksamensprojekt
{
    public class ServiceItem
    {
        private int Amount { get; set; }


        public ServiceItem(int amount)
        {
            Amount = amount;
        }

        public ServiceItem() { }
    }
    public class ServiceItemList
    {
        public List<ServiceItem> ServiceItems { get; set; }
        public ServiceItemList()
        {
            ServiceItems = new List<ServiceItem>();
        }
    }
}
