using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    public class GetAllEmployeesModel : PageModel
    {
        private IEmployeeService _employeeService;
        public GetAllEmployeesModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // TODO: Implement the logic to get all employees from Employee
        public List<Employee>? Employees { get; private set; }

        public void OnGet()
        {
            Employees = _employeeService.GetEmployees();
        }

        //public IActionResult OnPost()
        //{
        //}
    }
}
