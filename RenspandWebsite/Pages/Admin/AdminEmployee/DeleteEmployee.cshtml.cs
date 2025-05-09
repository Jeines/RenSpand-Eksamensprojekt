using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    public class DeleteEmployeeModel : PageModel
    {
        private IEmployeeService _employeeService;
        public DeleteEmployeeModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [BindProperty]
        public Employee Employee { get; set; }
        public IActionResult OnGet(int id)
        {
            Employee = _employeeService.GetEmployee(id);
            if (Employee == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }

        public IActionResult OnPost()
        {
            RenSpand_Eksamensprojekt.Employee deletedEmployee = _employeeService.DeleteEmployee(Employee.Id);
            if (deletedEmployee == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu
            return RedirectToPage("GetAllEmployees");
        }
    }
}
