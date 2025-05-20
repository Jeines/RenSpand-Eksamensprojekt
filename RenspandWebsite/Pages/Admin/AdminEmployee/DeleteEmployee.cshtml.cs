using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    [Authorize(Roles = "admin")]
    /// <summary>  
    /// Denne klasse h�ndterer sletning af en medarbejder.  
    /// </summary>  
    public class DeleteEmployeeModel : PageModel
    {
        /// <summary>  
        /// Medarbejder-servicen, der bruges til at administrere medarbejderdata.  
        /// </summary>  
        private IEmployeeService _employeeService;

        /// <summary>  
        /// Initialiserer en ny instans af DeleteEmployeeModel-klassen.  
        /// </summary>  
        /// <param name="employeeService">Servicen til medarbejderh�ndtering.</param>  
        public DeleteEmployeeModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>  
        /// Repr�senterer den medarbejder, der skal slettes.  
        /// </summary>  
        [BindProperty]
        public RenSpand_Eksamensprojekt.Employee Employee { get; set; }

        /// <summary>  
        /// H�ndterer GET-anmodningen for at slette en medarbejder.  
        /// </summary>  
        /// <param name="id">Id'et p� medarbejderen, der skal slettes.</param>  
        /// <returns>En IActionResult, der repr�senterer resultatet af operationen.</returns>  
        public IActionResult OnGet(int id)
        {
            Employee = _employeeService.GetEmployee(id);
            if (Employee == null)
                return RedirectToPage("/NotFound"); // NotFound er ikke defineret endnu  

            return Page();
        }

        /// <summary>  
        /// H�ndterer POST-anmodningen for at slette en medarbejder.  
        /// </summary>  
        /// <returns>En IActionResult, der repr�senterer resultatet af operationen.</returns>  
        public IActionResult OnPost()
        {
            RenSpand_Eksamensprojekt.Employee deletedEmployee = _employeeService.DeleteEmployee(Employee.Id);
            if (deletedEmployee == null)
                return RedirectToPage("/NotFound"); // NotFound er ikke defineret endnu  

            return RedirectToPage("GetAllEmployees");
        }
    }
}
