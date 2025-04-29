using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenSpand_Eksamensprojekt
{
    public enum Role
    {
        [Description("Administrator")]
        Admin,
        
        [Description("Employee")]
        Employee,
        
        [Description("Business")]
        Business,
        
        [Description("Private")]
        Private,
        
        [Description("Guest")]
        Guest

    }
}
