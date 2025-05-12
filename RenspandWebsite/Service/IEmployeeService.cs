using RenSpand_Eksamensprojekt;

namespace RenspandWebsite.Service
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployees();
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        Employee GetEmployee(int id);
        Employee DeleteEmployee(int? employeeId);

    }
}
