using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service.EmployeeServices;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    [Authorize(Roles = "admin")]
    /// <summary>
    /// Denne klasse håndterer oprettelsen af en ny medarbejder.
    /// </summary>
    public class CreateEmployeeModel : PageModel
    {
        private readonly EmployeeService _employeeService;

        public CreateEmployeeModel(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Repræsenterer medarbejderen der skal oprettes.
        /// </summary>
        [BindProperty]
        public RenSpand_Eksamensprojekt.Employee Employee { get; set; }

        /// <summary>
        /// Repræsenterer medarbejderens kvalifikationer som en kommasepareret streng.
        /// </summary>
        [BindProperty]
        public string EmployeeQualificationsString { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }


        /// <summary>
        /// Håndterer POST-anmodningen til oprettelse af medarbejder.
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

            // Email validering
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            if (!validateEmailRegex.IsMatch(Employee.Email))
            {
                ModelState.AddModelError("Email", "Invalid email format.");
                return Page();
            }

            // Kvalifikationer
            var kvalifikationer = new List<string>();
            if (!string.IsNullOrWhiteSpace(EmployeeQualificationsString))
            {
                var split = EmployeeQualificationsString.Split(',');
                foreach (var k in split)
                {
                    var trimmed = k.Trim();
                    if (!string.IsNullOrEmpty(trimmed))
                    {
                        kvalifikationer.Add(trimmed);
                    }
                }
            }

            // Hash password
            var passwordHasher = new PasswordHasher<RenSpand_Eksamensprojekt.Employee>();
            string hashedPassword = passwordHasher.HashPassword(null, Employee.Password);

            // Opret nyt Employee-objekt
            var newEmployee = new RenSpand_Eksamensprojekt.Employee
            {
                Username = Employee.Username,
                Password = hashedPassword,
                Name = Employee.Name,
                Email = Employee.Email,
                PhoneNumber = Employee.PhoneNumber,
                Role = RoleEnum.Employee,
                Salary = Employee.Salary,
                YearsOfExperians = Employee.YearsOfExperians,
                Qualifications = kvalifikationer
            };

            // Tilføj medarbejder til databasen
            await _employeeService.AddEmployeeAsync(newEmployee);
            // Redirect til siden med alle medarbejdere
            return RedirectToPage("/Admin/AdminEmployee/GetAllEmployees");
        }
    }
}
