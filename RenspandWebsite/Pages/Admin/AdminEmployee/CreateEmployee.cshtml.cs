using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;
using System.Collections.Generic;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    public class CreateEmployeeModel : PageModel
    {
        private IEmployeeService _EmployeeService;
        
        public CreateEmployeeModel(IEmployeeService employeeService)
        {
            _EmployeeService = employeeService;
        }

        [BindProperty]
        public Employee Employee { get; set; }
        [BindProperty]
        public string EmployeeQualificationsString { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                //test for at se hvis der er fejl
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"Fejl i felt '{modelState.Key}': {error.ErrorMessage}");
                    }
                }
                return Page();
            }
            // Check Employee er null
            if (Employee.Qualifications == null)
            {
                // laver en list if den er null
                Employee.Qualifications = new List<string>();
            }
           
            List<string> kval = new List<string>();

            // hvis EmployeeQualificationsString ikker er null eller tom
            if (!string.IsNullOrWhiteSpace(EmployeeQualificationsString))
            {
                //Opdel strengen med komma og fjern mellemrum fra hver kvalifikation
                var split = EmployeeQualificationsString.Split(',');
                foreach (var k in split)
                    kval.Add(k.Trim());
            }

            // Opretter et nyt Employee-objekt med de indtastede værdier
            Employee = new RenSpand_Eksamensprojekt.Employee
            {
                Id = Employee.Id,
                Username = Employee.Username,
                Password = Employee.Password,
                Name = Employee.Name,
                Email = Employee.Email,
                PhoneNumber = Employee.PhoneNumber,
                Role = RoleEnum.Employee,
                Salary = Employee.Salary,
                YearsOfExperians = Employee.YearsOfExperians,
                Qualifications = kval
            };
            // Tilføjer den nye medarbejder til listen og gemmer i JSON
            _EmployeeService.AddEmployee(Employee);
            // Sender brugeren til oversigten over alle medarbejdere
            return RedirectToPage("/Admin/AdminEmployee/GetAllEmployees");
        }

    }
}
