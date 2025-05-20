using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service.EmployeeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    [Authorize(Roles = "admin")]
    /// <summary>
    /// Denne klasse håndterer redigering af en medarbejder.
    /// </summary>
    public class EditEmployeeModel : PageModel
    {
        private readonly EmployeeService _employeeService;

        public EditEmployeeModel(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [BindProperty]
        public RenSpand_Eksamensprojekt.Employee Employee { get; set; }

        [BindProperty]
        public string EmployeeQualificationsString { get; set; }

        private string _originalPassword;

        /// <summary>
        /// Håndterer GET-anmodningen til redigering af medarbejder.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Employee = await _employeeService.GetEmployeeAsync(id);
            if (Employee == null)
                return RedirectToPage("/NotFound");

            //_originalPassword = Employee.Password;
            EmployeeQualificationsString = string.Join(", ", Employee.Qualifications ?? new List<string>());
            return Page();
        }

        /// <summary>
        /// Håndterer POST-anmodningen til opdatering af medarbejder.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
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

            // Hent eksisterende medarbejder fra DB for at bevare originalt password
            var existingEmployee = await _employeeService.GetEmployeeAsync(Employee.Id);
            if (existingEmployee == null)
                return RedirectToPage("/NotFound");

            // Opdater medarbejderens oplysninger
            existingEmployee.Username = Employee.Username;
            existingEmployee.Name = Employee.Name;
            existingEmployee.Email = Employee.Email;
            existingEmployee.PhoneNumber = Employee.PhoneNumber;
            existingEmployee.Username = Employee.Username;
            existingEmployee.YearsOfExperians = Employee.YearsOfExperians;
            existingEmployee.Salary = Employee.Salary;

            // Håndtering af kvalifikationer
            if (!string.IsNullOrWhiteSpace(EmployeeQualificationsString))
            {
                existingEmployee.Qualifications = EmployeeQualificationsString
                    .Split(',')
                    .Select(q => q.Trim())
                    .Where(q => !string.IsNullOrEmpty(q))
                    .ToList();
            }
            else
            {
                existingEmployee.Qualifications = new List<string>();
            }

            // Bevar originalt password hvis det ikke er ændret via UI
            Employee.Password = existingEmployee.Password;


            await _employeeService.UpdateEmployeeAsync(existingEmployee);
            return RedirectToPage("GetAllEmployees");
        }
    }
}


