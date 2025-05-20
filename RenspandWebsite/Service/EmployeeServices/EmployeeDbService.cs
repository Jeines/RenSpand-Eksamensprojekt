using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tls;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;

namespace RenspandWebsite.Service.EmployeeServices
{
    public class EmployeeDbService : DbService<Employee>
    {
        /// <summary>
        /// Denne klasse håndterer databaseoperationer for medarbejdere.
        /// </summary>
        private RenSpandDbContext _context = new RenSpandDbContext();
        public EmployeeDbService() { }
        public EmployeeDbService(RenSpandDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Henter en medarbejder fra databasen med det givne id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        /// <summary>
        /// Henter alle medarbejdere fra databasen.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        /// <summary>
        /// Tilføjer en medarbejder til databasen.
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public async Task AddEmployeeAsync(Employee emp)
        {
            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Opdaterer en medarbejder i databasen.
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public async Task UpdateEmployeeAsync(Employee emp)
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Sletter en medarbejder fra databasen med det givne id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteEmployeeAsync(int id)
        {
            // Find medarbejderen i databasen
            var employee = await _context.Employees.FindAsync(id);
            // Hvis medarbejderen findes, slet den
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            //else
            //{
            //    throw new Exception("Employee not found");
            //}
        }


    }
}
