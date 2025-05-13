using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    /// <summary>
    /// This class handles the retrieval of all employees.
    /// </summary>
    public class GetAllEmployeesModel : PageModel
    {
        /// <summary>
        /// The employee service used to manage employee data.
        /// </summary>
        private IEmployeeService _employeeService;

        /// <summary>
        /// Initializes a new instance of the GetAllEmployeesModel class.
        /// </summary>
        /// <param name="employeeService"></param>
        public GetAllEmployeesModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Represents the list of employees.
        /// </summary>
        public List<RenSpand_Eksamensprojekt.Employee>? Employees { get; private set; }

        /// <summary>
        /// Handles the GET request for retrieving all employees.
        /// </summary>
        public void OnGet()
        {
            Employees = _employeeService.GetEmployees();
        }
    }
}
