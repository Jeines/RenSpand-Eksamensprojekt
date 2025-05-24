using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service.EmployeeServices;
using System.Threading.Tasks;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{

    [Authorize(Roles = "admin")]
    /// <summary>
    /// Denne klasse håndterer sletning af en medarbejder.
    /// </summary>
    public class DeleteEmployeeModel : PageModel
    {
        private readonly EmployeeService _employeeService;

        /// <summary>
        /// Konstruktør for DeleteEmployeeModel.
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
        public Models.Employee Employee { get; set; }

        /// <summary>
        /// Håndterer GET-anmodningen for sletning af medarbejder.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Hent medarbejderen fra databasen
            Employee = await _employeeService.GetEmployeeAsync(id);
            // Hvis medarbejderen ikke findes, returner til NotFound siden
            if (Employee == null)
                return RedirectToPage("/NotFound"); // Husk at oprette denne side

            // Returner til siden med medarbejderoplysninger
            return Page();
        }

        /// <summary>
        /// Håndterer POST-anmodningen for sletning af medarbejder.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            // Validering af model
            if (!ModelState.IsValid)
            {
                // Log fejlene
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"Fejl i felt '{modelState.Key}': {error.ErrorMessage}");
                    }
                }
                return Page();
            }
            // Hent medarbejderen fra databasen
            var employee = await _employeeService.GetEmployeeAsync(Employee.Id);

            // Hvis medarbejderen ikke findes, returner til NotFound siden
            if (employee == null)
                return RedirectToPage("/NotFound"); // Husk at oprette denne side

            // Slet medarbejderen
            await _employeeService.DeleteEmployeeAsync(Employee.Id);
            return RedirectToPage("GetAllEmployees");
        }
    }
}
