using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service.EmployeeServices;
using System.Threading.Tasks;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    [Authorize(Roles = "admin")]
    /// <summary>
    /// Denne klasse h�ndterer sletning af en medarbejder.
    /// </summary>
    public class DeleteEmployeeModel : PageModel
    {
        private readonly EmployeeService _employeeService;

        /// <summary>
        /// Konstrukt�r for DeleteEmployeeModel.
        /// </summary>
        /// <param name="employeeService"></param>
        public DeleteEmployeeModel(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Den medarbejder der skal slettes.
        /// </summary>
        [BindProperty]
        public RenSpand_Eksamensprojekt.Employee Employee { get; set; }

        /// <summary>
        /// H�ndterer GET-anmodningen for sletning af medarbejder.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Employee = await _employeeService.GetEmployeeAsync(id);
            if (Employee == null)
                return RedirectToPage("/NotFound"); // Husk at oprette denne side

            return Page();
        }

        /// <summary>
        /// H�ndterer POST-anmodningen for sletning af medarbejder.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            var employee = await _employeeService.GetEmployeeAsync(Employee.Id);
            if (employee == null)
                return RedirectToPage("/NotFound"); // Husk at oprette denne side

            await _employeeService.DeleteEmployeeAsync(Employee.Id);
            return RedirectToPage("GetAllEmployees");
        }
    }
}
