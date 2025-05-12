using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public List<RenSpand_Eksamensprojekt.Employee>? Employees { get; private set; }

        public void OnGet()
        {
            Employees = _employeeService.GetEmployees();
        }
    }
}
