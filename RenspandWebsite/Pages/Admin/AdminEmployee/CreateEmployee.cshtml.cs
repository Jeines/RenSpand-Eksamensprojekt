using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;
using System.Collections.Generic;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    /// <summary>
    /// This class handles the creation of a new employee.
    /// </summary>
    public class CreateEmployeeModel : PageModel
    {
        /// <summary>
        /// The employee service used to manage employee data.
        /// </summary>
        private IEmployeeService _EmployeeService;

        //TODO : ADD Profile
        //private ProfileService _profileService;
        // ProfileService profileService

        /// <summary>
        /// Initializes a new instance of the CreateEmployeeModel class.
        /// </summary>
        /// <param name="employeeService"></param>
        public CreateEmployeeModel(IEmployeeService employeeService)
        {
            
            _EmployeeService = employeeService;
            //_profileService = profileService;
        }

        /// <summary>
        /// Represents the employee to be created.
        /// </summary>
        [BindProperty]
        public RenSpand_Eksamensprojekt.Employee Employee { get; set; }

        /// <summary>
        /// Represents the profile of the employee.
        /// </summary>
        public Profile Profile { get; set; }

        /// <summary>
        /// A string representation of the employee's qualifications.
        /// </summary>
        [BindProperty]
        public string EmployeeQualificationsString { get; set; }

        /// <summary>
        /// Handles the GET request for creating a new employee.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGet()
        {
            return Page();
        }
        /// <summary>
        /// Handles the POST request for creating a new employee.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Hvis der er fejl i modelstate, så udskriv fejlene til konsollen
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"Fejl i felt '{modelState.Key}': {error.ErrorMessage}");
                    }
                }
                // Returner til den samme side for at vise fejlene
                return Page();
            }
            // Check Employee er null
            if (Employee.Qualifications == null)
            {
                // laver en list if den er null
                Employee.Qualifications = new List<string>();
            }
            // Opretter en liste til kvalifikationer
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

            // Opretter et nyt Profile-objekt med de indtastede værdier
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
            // TODO: ADD Profile
            //_profileService.AddProfile(Profile);
            // Sender brugeren til oversigten over alle medarbejdere
            return RedirectToPage("/Admin/AdminEmployee/GetAllEmployees");
        }

    }
}
