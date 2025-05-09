using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public IActionResult OnGet(int id)
        {
            Employee = _employeeService.GetEmployee(id);
            EmployeeQualificationsString = string.Join(", ", Employee.Qualifications);
            if (Employee == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
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

            _employeeService.UpdateEmployee(Employee);
            return RedirectToPage("GetAllEmployees");
        }
    }
}
