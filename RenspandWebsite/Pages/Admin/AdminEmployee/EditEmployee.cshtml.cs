using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;

namespace RenspandWebsite.Pages.Admin.AdminEmployee
{
    public class EditEmployeeModel : PageModel
    {
        private IEmployeeService _employeeService;
        public EditEmployeeModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [BindProperty]
        public RenSpand_Eksamensprojekt.Employee Employee { get; set; }

        

        [BindProperty]
        public string EmployeeQualificationsString { get; set; }

        private string _originalPassword;
        public IActionResult OnGet(int id)
        {
            Employee = _employeeService.GetEmployee(id);
            if (Employee == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            _originalPassword = Employee.Password;
            EmployeeQualificationsString = string.Join(", ", Employee.Qualifications);
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
                Console.WriteLine(Employee.ToString());
                return Page();
            }

            // Check EmployeeQualificationsString ikke er null
            if (!string.IsNullOrWhiteSpace(EmployeeQualificationsString))
            {
                string[] split = EmployeeQualificationsString.Split(',');
                List<string> trimmedList = new List<string>();

                foreach (string q in split)
                {
                    string trimmed = q.Trim(); // Fjern mellemrum før/efter
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
            var existingEmployee = _employeeService.GetEmployee(Employee.Id);
            Employee.Password = existingEmployee.Password;
            _employeeService.UpdateEmployee(Employee);
            return RedirectToPage("GetAllEmployees");
            
        }
    }
}
