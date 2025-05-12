using RenSpand_Eksamensprojekt;

namespace RenspandWebsite.Service
{
    public class EmployeeService : IEmployeeService
    {
        private JsonFileService<Employee> JsonFileService { get; set; }
        private List<Employee> _employees;

        public EmployeeService(JsonFileService<Employee> jsonFileService)
        {
            JsonFileService = jsonFileService;
            _employees = JsonFileService.GetJsonObjects().ToList();
        }
        public void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
            JsonFileService.SaveJsonObjects(_employees);
        }

        public Employee DeleteEmployee(int? employeeId)
        {
            Employee empToBeDeleted = null;
            foreach (Employee e in _employees)
            {
                if (e.Id == employeeId)
                {
                    empToBeDeleted = e;
                    break;
                }
            }

            if (empToBeDeleted != null)
            {
                _employees.Remove(empToBeDeleted);
                JsonFileService.SaveJsonObjects(_employees);
            }

            return empToBeDeleted;
        }

        public Employee GetEmployee(int id)
        {
            foreach (Employee e in _employees)
            {
                if (e.Id == id)
                    return e;
            }

            return null;
        }

        public List<Employee> GetEmployees()
        {
            return _employees;
        }

        public void UpdateEmployee(Employee employee)
        {
            if (employee != null)
            {
                foreach (Employee e in _employees)
                {
                    if (e.Id == employee.Id)
                    {
                        e.Name = employee.Name;
                        e.Email = employee.Email;
                        e.PhoneNumber = employee.PhoneNumber;
                        e.YearsOfExperians = employee.YearsOfExperians;
                        e.Salary = employee.Salary;
                        e.Qualifications = employee.Qualifications;
                    }
                }
                JsonFileService.SaveJsonObjects(_employees);
            }
        }
    }
}
