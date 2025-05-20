using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;
using System.Text.RegularExpressions;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    [Authorize(Roles = "admin")]
    /// <summary>  
    /// Denne klasse håndterer oprettelsen af en ny medarbejder.  
    /// </summary>  
    public class CreateEmployeeModel : PageModel
    {
        /// <summary>  
        /// Tjenesten til håndtering af medarbejderdata.  
        /// </summary>  
        private IEmployeeService _EmployeeService;

        //TODO : Tilføj profil  
        //private ProfileService _profileService;  
        // ProfileService profileService  

        /// <summary>  
        /// Initialiserer en ny instans af CreateEmployeeModel-klassen.  
        /// </summary>  
        /// <param name="employeeService">Tjenesten til medarbejderhåndtering.</param>  
        public CreateEmployeeModel(IEmployeeService employeeService)
        {
            _EmployeeService = employeeService;
            //_profileService = profileService;  
        }

        /// <summary>  
        /// Repræsenterer den medarbejder, der skal oprettes.  
        /// </summary>  
        [BindProperty]
        public RenSpand_Eksamensprojekt.Employee Employee { get; set; }

        /// <summary>  
        /// Repræsenterer medarbejderens profil.  
        /// </summary>  
        public Profile Profile { get; set; }

        /// <summary>  
        /// En strengrepræsentation af medarbejderens kvalifikationer.  
        /// </summary>  
        [BindProperty]
        public string EmployeeQualificationsString { get; set; }

        /// <summary>  
        /// Håndterer GET-anmodningen for oprettelse af en ny medarbejder.  
        /// </summary>  
        /// <returns>Returnerer siden.</returns>  
        public IActionResult OnGet()
        {
            return Page();
        }

        /// <summary>  
        /// Håndterer POST-anmodningen for oprettelse af en ny medarbejder.  
        /// </summary>  
        /// <returns>Returnerer en side eller omdirigerer til en anden side.</returns>  
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Hvis der er fejl i ModelState, udskriv fejlene til konsollen  
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

            // Tjek om Employee er null  
            if (Employee.Qualifications == null)
            {
                // Opret en liste, hvis den er null  
                Employee.Qualifications = new List<string>();
            }

            // Opret en liste til kvalifikationer  
            List<string> kval = new List<string>();

            // Hvis EmployeeQualificationsString ikke er null eller tom  
            if (!string.IsNullOrWhiteSpace(EmployeeQualificationsString))
            {
                // Opdel strengen med komma og fjern mellemrum fra hver kvalifikation  
                var split = EmployeeQualificationsString.Split(',');
                foreach (var k in split)
                    kval.Add(k.Trim());
            }

            // Hash password  
            var passwordHasher = new PasswordHasher<RenSpand_Eksamensprojekt.Employee>();
            string hashedPassword = passwordHasher.HashPassword(null, Employee.Password);

            // Tjek om email er i gyldigt format  
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            if (!validateEmailRegex.IsMatch(Employee.Email))
            {
                ModelState.AddModelError("Email", "Ugyldigt email-format.");
                return Page();
            }

            // Opret en ny Employee-instans med de indtastede værdier  
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

            // Opret en ny Profile-instans med de indtastede værdier  
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

            // Tilføj den nye medarbejder til listen og gem i JSON  
            _EmployeeService.AddEmployee(Employee);
            // Tilføj den nye profil til listen og gem i JSON  
            // TODO: Tilføj profil  
            //_profileService.AddProfile(Profile);  

            // Omdiriger brugeren til oversigten over alle medarbejdere  
            return RedirectToPage("/Admin/AdminEmployee/GetAllEmployees");
        }
    }
}
