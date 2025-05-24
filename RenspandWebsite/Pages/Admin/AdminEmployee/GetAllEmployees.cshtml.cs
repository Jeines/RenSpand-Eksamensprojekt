using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service.EmployeeServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    [Authorize(Roles = "admin")]
    /// <summary>
    /// Denne klasse håndterer hentningen af alle medarbejdere.
    /// </summary>
    public class GetAllEmployeesModel : PageModel
    {
        private readonly EmployeeService _employeeService;

        public GetAllEmployeesModel(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Repræsenterer listen af medarbejdere.
        /// </summary>
        public List<Models.Employee>? Employees { get; private set; }

        /// <summary>
        /// Håndterer GET-anmodningen for at hente alle medarbejdere.
        /// </summary>
        public async Task OnGetAsync()
        {
            Employees = await _employeeService.GetEmployeesAsync();
        }
    }
}
