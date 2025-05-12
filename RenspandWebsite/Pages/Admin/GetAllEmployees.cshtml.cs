using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Admin
{
    public class GetAllEmployeesModel : PageModel
    {
        private IEmployeeService _employeeService;
        public GetAllEmployeesModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public List<Employee>? Employees { get; private set; }

        public void OnGet()
        {
            Employees = _employeeService.GetEmployees();
        }
    }
}
