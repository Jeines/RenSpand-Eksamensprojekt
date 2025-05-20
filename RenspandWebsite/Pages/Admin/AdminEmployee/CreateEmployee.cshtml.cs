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

        [BindProperty]
        public RenSpand_Eksamensprojekt.Employee Employee { get; set; }

        public Profile Profile { get; set; }

        [BindProperty]
        public string EmployeeQualificationsString { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
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

            await _employeeService.AddEmployeeAsync(newEmployee);

            // TODO: Tilføj oprettelse af Profile hvis nødvendigt via en separat ProfileService

            return RedirectToPage("/Admin/AdminEmployee/GetAllEmployees");
        }
    }
}
