using RenSpand_Eksamensprojekt;

namespace RenspandWebsite.Service
{
    /// <summary>
    /// Interface til EmployeeService, der definerer metoder til håndtering af medarbejdere.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Henter en liste af alle medarbejdere.
        /// </summary>
        /// <returns>Liste af <see cref="Employee"/> objekter.</returns>
        List<Employee> GetEmployees();

        /// <summary>
        /// Tilføjer en ny medarbejder.
        /// </summary>
        /// <param name="employee">Medarbejderen der skal tilføjes.</param>
        void AddEmployee(Employee employee);

        /// <summary>
        /// Opdaterer en eksisterende medarbejder.
        /// </summary>
        /// <param name="employee">Medarbejderen med opdaterede oplysninger.</param>
        void UpdateEmployee(Employee employee);

        /// <summary>
        /// Henter en medarbejder ud fra et id.
        /// </summary>
        /// <param name="id">Id på medarbejderen.</param>
        /// <returns>Et <see cref="Employee"/> objekt eller null hvis ikke fundet.</returns>
        Employee GetEmployee(int id);

        /// <summary>
        /// Sletter en medarbejder ud fra et id.
        /// </summary>
        /// <param name="employeeId">Id på medarbejderen der skal slettes.</param>
        /// <returns>Den slettede <see cref="Employee"/> eller null hvis ikke fundet.</returns>
        Employee DeleteEmployee(int? employeeId);
    }
}
