using System.ComponentModel;

namespace RenSpand_Eksamensprojekt
{
    public enum RoleEnum
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
