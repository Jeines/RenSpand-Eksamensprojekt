using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    [Authorize(Roles = "admin")]
    /// <summary>
    /// This class handles the deletion of an employee.
    /// </summary>
    public class DeleteEmployeeModel : PageModel
    {
        /// <summary>
        /// The employee service used to manage employee data.
        /// </summary>
        private IEmployeeService _employeeService;
        /// <summary>
        /// Initializes a new instance of the DeleteEmployeeModel class.
        /// </summary>
        /// <param name="employeeService"></param>
        public DeleteEmployeeModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        /// <summary>
        /// Represents the employee to be deleted.
        /// </summary>
        [BindProperty]
        public RenSpand_Eksamensprojekt.Employee Employee { get; set; }

        /// <summary>
        /// Handles the GET request for deleting an employee.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult OnGet(int id)
        {
            Employee = _employeeService.GetEmployee(id);
            if (Employee == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }

        /// <summary>
        /// Handles the POST request for deleting an employee.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            RenSpand_Eksamensprojekt.Employee deletedEmployee = _employeeService.DeleteEmployee(Employee.Id);
            if (deletedEmployee == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu
            return RedirectToPage("GetAllEmployees");
        }
    }
}
