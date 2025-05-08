using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenSpand_Eksamensprojekt
{
    public class ServiceItem
    {
        public int Amount { get; set; }

        public Work ServiceWork { get; set; }

        public ServiceItem(int amount, Work serviceWork)
        {
            Amount = amount;
            ServiceWork = serviceWork;
        }

        public ServiceItem() { }
    }
}
