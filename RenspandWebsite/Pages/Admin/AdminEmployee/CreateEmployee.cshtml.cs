using Microsoft.AspNetCore.Identity;
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
        private ProfileService _profileService;

        public CreateEmployeeModel(IEmployeeService employeeService, ProfileService profileService)
        {
            _EmployeeService = employeeService;
            _profileService = profileService;
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
            // hash password
            var passwordHasher = new PasswordHasher<RenSpand_Eksamensprojekt.Employee>();
            string hashedPassword = passwordHasher.HashPassword(null, Employee.Password);

            // Opretter et nyt Employee-objekt med de indtastede værdier
            Employee = new RenSpand_Eksamensprojekt.Employee
            {
                Id = Employee.Id,
                Username = Employee.Username,
                Password = hashedPassword,
                Name = Employee.Name,
                Email = Employee.Email,
                PhoneNumber = Employee.PhoneNumber,
                Role = RoleEnum.Employee,
                Salary = Employee.Salary,
                YearsOfExperians = Employee.YearsOfExperians,
                Qualifications = kval
            };

            Profile = new Profile
            {
                Id = Employee.Id,
                Username = Employee.Username,
                Password = hashedPassword,
                Name = Employee.Name,
                Email = Employee.Email,
                PhoneNumber = Employee.PhoneNumber,
                Role = RoleEnum.Employee
            };

            // Tilføjer den nye medarbejder til listen og gemmer i JSON
            _EmployeeService.AddEmployee(Employee);
            // Tilføjer den nye profil til listen og gemmer i JSON
            _profileService.AddProfile(Profile);
            // Sender brugeren til oversigten over alle medarbejdere
            return RedirectToPage("/Admin/AdminEmployee/GetAllEmployees");
        }

    }
}
