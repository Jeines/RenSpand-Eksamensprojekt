
using RenspandWebsite.Models;

namespace RenspandWebsite.Service.EmployeeServices
{
    public class EmployeeService
    {
        /// <summary>
        /// Denne klasse håndterer alle operationer relateret til medarbejdere.
        /// </summary>
        private readonly EmployeeDbService _employeeDbService;

        /// <summary>
        /// Konstruktøren til EmployeeService.
        /// </summary>
        /// <param name="employeeDbService"></param>
        public EmployeeService(EmployeeDbService employeeDbService)
        {
            _employeeDbService = employeeDbService;
        }

        public EmployeeService() { }

        /// <summary>
        /// Henter alle medarbejdere fra databasen.
        /// </summary>
        /// <returns></returns>
        
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _employeeDbService.GetObjectsAsync();
        }
        //public async Task<List<Employee>> GetEmployeesAsync()
        //{
        //    return await _employeeDbService.GetObjectsAsync;
        //}

        /// <summary>
        /// Henter en medarbejder fra databasen med det givne id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeeAsync(int id)
        {
            return await _employeeDbService.GetObjectByIdAsync(id);
        }

        /// <summary>
        /// Tilføjer en medarbejder til databasen.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task AddEmployeeAsync(Employee employee)
        {
            await _employeeDbService.AddObjectAsync(employee);
        }

        /// <summary>
        /// Opdaterer en medarbejder i databasen.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _employeeDbService.UpdateObjectAsync(employee);
        }

        /// <summary>
        /// Sletter en medarbejder fra databasen med det givne id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteEmployeeAsync(int id)
        {
            await _employeeDbService.DeleteObjectAsync(id);
        }

    }
}
