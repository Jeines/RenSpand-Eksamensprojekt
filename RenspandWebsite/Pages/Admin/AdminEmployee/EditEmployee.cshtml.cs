using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    [Authorize(Roles = "admin")]
    /// <summary>
    /// This class handles the editing of an employee.
    /// </summary>
    public class EditEmployeeModel : PageModel
    {
        /// <summary>
        /// The employee service used to manage employee data.
        /// </summary>
        private IEmployeeService _employeeService;

        /// <summary>
        /// Initializes a new instance of the EditEmployeeModel class.
        /// </summary>
        /// <param name="employeeService"></param>
        public EditEmployeeModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Represents the employee to be edited.
        /// </summary>
        [BindProperty]
        public RenSpand_Eksamensprojekt.Employee Employee { get; set; }

        

        [BindProperty]
        public string EmployeeQualificationsString { get; set; }

        /// <summary>
        /// The original password of the employee.
        /// </summary>
        private string _originalPassword;

        /// <summary>
        /// Handles the GET request for editing an employee.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult OnGet(int id)
        {
            Employee = _employeeService.GetEmployee(id);
            if (Employee == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            _originalPassword = Employee.Password;
            EmployeeQualificationsString = string.Join(", ", Employee.Qualifications);
            return Page();
        }

        /// <summary>
        /// Handles the POST request for editing an employee.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Log the errors to the console
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"Fejl i felt '{modelState.Key}': {error.ErrorMessage}");
                    }
                }
                return Page();
            }

            // Check if the password has changed
            if (!string.IsNullOrWhiteSpace(EmployeeQualificationsString))
            {
                // Split the string by comma and trim each qualification
                string[] split = EmployeeQualificationsString.Split(',');
                // Create a new list to hold the trimmed qualifications
                List<string> trimmedList = new List<string>();

                // Iterate through the split qualifications and trim them
                foreach (string q in split)
                {
                    // Trim each qualification and check if it's not empty
                    string trimmed = q.Trim();
                    if (!string.IsNullOrEmpty(trimmed))
                    {
                        // Add the trimmed qualification to the list
                        trimmedList.Add(trimmed);
                    }
                }
                // Assign the trimmed list to the Employee's Qualifications property
                Employee.Qualifications = trimmedList;
            }
            // If the qualifications string is null or empty, assign an empty list
            else
            
            {
                Employee.Qualifications = new List<string>();
            }
            // If the password has not changed, keep the original password
            var existingEmployee = _employeeService.GetEmployee(Employee.Id);
            Employee.Password = existingEmployee.Password;
            // Update the employee in the database/JSON
            _employeeService.UpdateEmployee(Employee);
            return RedirectToPage("GetAllEmployees");

        }
    }
}
