using RenSpand_Eksamensprojekt;

namespace RenspandWebsite.Service
{
    public class EmployeeService : IEmployeeService
    {
        private JsonFileService<Employee> JsonFileService { get; set; }
        private List<Employee> _employees;

        /// <summary>
        /// Initialiserer en ny instans af EmployeeService og indlæser medarbejdere fra JSON-filen.
        /// </summary>
        /// <param name="jsonFileService">Service til håndtering af JSON-filer.</param>
        public EmployeeService(JsonFileService<Employee> jsonFileService)
        {
            JsonFileService = jsonFileService;
            _employees = JsonFileService.GetJsonObjects().ToList();
        }

        /// <summary>
        /// Tilføjer en medarbejder og gemmer listen til JSON-filen.
        /// </summary>
        /// <param name="employee">Medarbejderen der skal tilføjes.</param>
        public void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
            JsonFileService.SaveJsonObjects(_employees);
        }

        /// <summary>
        /// Sletter en medarbejder ud fra ID og gemmer listen til JSON-filen.
        /// </summary>
        /// <param name="employeeId">ID på medarbejderen der skal slettes.</param>
        /// <returns>Den slettede medarbejder, eller null hvis ikke fundet.</returns>
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

        /// <summary>
        /// Henter en medarbejder ud fra ID.
        /// </summary>
        /// <param name="id">ID på medarbejderen.</param>
        /// <returns>Medarbejderen hvis fundet, ellers null.</returns>
        public Employee GetEmployee(int id)
        {
            foreach (Employee e in _employees)
            {
                if (e.Id == id)
                    return e;
            }

            return null;
        }

        /// <summary>
        /// Henter alle medarbejdere.
        /// </summary>
        /// <returns>Liste af medarbejdere.</returns>
        public List<Employee> GetEmployees()
        {
            return _employees;
        }

        /// <summary>
        /// Opdaterer en medarbejders oplysninger og gemmer listen til JSON-filen.
        /// </summary>
        /// <param name="employee">Medarbejderen med opdaterede oplysninger.</param>
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
