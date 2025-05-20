using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    [Authorize(Roles = "admin")]
    /// <summary>
    /// Denne klasse håndterer hentning af alle medarbejdere.
    /// </summary>
    public class GetAllEmployeesModel : PageModel
    {
        /// <summary>
        /// Employee-servicen der bruges til at administrere medarbejderdata.
        /// </summary>
        private IEmployeeService _employeeService;

        /// <summary>
        /// Initialiserer en ny instans af GetAllEmployeesModel-klassen.
        /// </summary>
        /// <param name="employeeService">Service til håndtering af medarbejdere.</param>
        public GetAllEmployeesModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Listen over alle medarbejdere.
        /// </summary>
        public List<RenSpand_Eksamensprojekt.Employee>? Employees { get; private set; }

        /// <summary>
        /// Håndterer GET-anmodningen for at hente alle medarbejdere.
        /// </summary>
        public void OnGet()
        {
            Employees = _employeeService.GetEmployees();
        }
    }
}
