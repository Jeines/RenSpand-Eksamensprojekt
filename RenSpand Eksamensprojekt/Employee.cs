using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenSpand_Eksamensprojekt
{
    public class Employee
    {
       public int YearsOfExperians { get; set; }
       
        public decimal Salary { get; set; }

        public List<string> Qualifications { get; set; } = new List<string>();

        public Employee(int yearsOfExperians, decimal salary, List<string> qualifications)
        {
            YearsOfExperians = yearsOfExperians;
            Salary = salary;
            Qualifications = qualifications;
        }

        public Employee() { }

    }
}
