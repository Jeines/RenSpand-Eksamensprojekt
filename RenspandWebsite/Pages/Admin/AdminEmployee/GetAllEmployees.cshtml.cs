using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service.EmployeeServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    [Authorize(Roles = "admin")]
    /// <summary>
    /// Denne klasse h�ndterer hentningen af alle medarbejdere.
    /// </summary>
    public class GetAllEmployeesModel : PageModel
    {
        private readonly EmployeeService _employeeService;

        public GetAllEmployeesModel(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Repr�senterer listen af medarbejdere.
        /// </summary>
        public List<RenSpand_Eksamensprojekt.Employee>? Employees { get; private set; }

        /// <summary>
        /// H�ndterer GET-anmodningen for at hente alle medarbejdere.
        /// </summary>
        public async Task OnGetAsync()
        {
            Employees = await _employeeService.GetEmployeesAsync();
        }
    }
}
