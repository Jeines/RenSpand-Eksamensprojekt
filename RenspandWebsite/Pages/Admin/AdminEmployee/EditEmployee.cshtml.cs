using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    [Authorize(Roles = "admin")]  

    public class EditEmployeeModel : PageModel
    {
        /// <summary>  
        /// Tjenesten til h�ndtering af medarbejderdata.  
        /// </summary>  
        private IEmployeeService _employeeService;

        /// <summary>  
        /// Initialiserer en ny instans af EditEmployeeModel-klassen.  
        /// </summary>  
        /// <param name="employeeService">Tjenesten til medarbejderh�ndtering.</param>  
        public EditEmployeeModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>  
        /// Repr�senterer den medarbejder, der skal redigeres.  
        /// </summary>  
        [BindProperty]
        public RenSpand_Eksamensprojekt.Employee Employee { get; set; }

        /// <summary>  
        /// Repr�senterer medarbejderens kvalifikationer som en kommasepareret streng.  
        /// </summary>  
        [BindProperty]
        public string EmployeeQualificationsString { get; set; }

        /// <summary>  
        /// Den oprindelige adgangskode for medarbejderen.  
        /// </summary>  
        private string _originalPassword;

        /// <summary>  
        /// H�ndterer GET-anmodningen for redigering af en medarbejder.  
        /// </summary>  
        /// <param name="id">Id'et p� den medarbejder, der skal redigeres.</param>  
        /// <returns>En IActionResult, der repr�senterer resultatet af anmodningen.</returns>  
        public IActionResult OnGet(int id)
        {
            Employee = _employeeService.GetEmployee(id);
            if (Employee == null)
                return RedirectToPage("/NotFound"); // NotFound-siden er ikke defineret endnu.  

            _originalPassword = Employee.Password;
            EmployeeQualificationsString = string.Join(", ", Employee.Qualifications);
            return Page();
        }

        /// <summary>  
        /// H�ndterer POST-anmodningen for redigering af en medarbejder.  
        /// </summary>  
        /// <returns>En IActionResult, der repr�senterer resultatet af anmodningen.</returns>  
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Log fejl til konsollen  
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"Fejl i felt '{modelState.Key}': {error.ErrorMessage}");
                    }
                }
                return Page();
            }

            // H�ndter kvalifikationer  
            if (!string.IsNullOrWhiteSpace(EmployeeQualificationsString))
            {
                // Split strengen ved komma og trim hver kvalifikation  
                string[] split = EmployeeQualificationsString.Split(',');
                List<string> trimmedList = new List<string>();

                foreach (string q in split)
                {
                    string trimmed = q.Trim();
                    if (!string.IsNullOrEmpty(trimmed))
                    {
                        trimmedList.Add(trimmed);
                    }
                }
                Employee.Qualifications = trimmedList;
            }
            else
            {
                Employee.Qualifications = new List<string>();
            }

            // Behold den oprindelige adgangskode, hvis den ikke er �ndret  
            var existingEmployee = _employeeService.GetEmployee(Employee.Id);
            Employee.Password = existingEmployee.Password;

            // Opdater medarbejderen i databasen/JSON  
            _employeeService.UpdateEmployee(Employee);
            return RedirectToPage("GetAllEmployees");
        }
    }
}
