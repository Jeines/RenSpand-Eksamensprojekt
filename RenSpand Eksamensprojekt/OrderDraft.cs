using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenSpand_Eksamensprojekt
{
    //class to transfer data from one site to another
    public class OrderDraft
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public List<int> SelectedWorkIds { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime TrashCanEmptyDate { get; set; }
    }

}
