using System.ComponentModel;

namespace RenSpand_Eksamensprojekt
{
    /// <summary>
    /// Viser de forskellige roller som en bruger kan have i systemet 
    /// og som kan være "Admin", "Employee", "Business", "Private" eller "Guest".
    /// </summary>
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
