using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenSpand_Eksamensprojekt
{
    public class Employee : Profile
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

        public override string ToString()
        {
            // Hvis der er nogle kvalifikationer, laver vi en tekst med dem adskilt med komma
            string kvalifikationerSomTekst = "";
            if (Qualifications != null)
            {
                kvalifikationerSomTekst = string.Join(", ", Qualifications);
            }
            else
            {
                // Hvis der ikke er nogen kvalifikationer, sætter vi teksten til "Ingen kvalifikationer"
                kvalifikationerSomTekst = "Ingen kvalifikationer";
            }

            // Vi laver en tekst som viser alle de vigtige oplysninger
            string tekst = "Erfaring: " + YearsOfExperians + " år\n";
            tekst += "Løn: " + Salary + " kr.\n";
            tekst += "Kvalifikationer: " + kvalifikationerSomTekst;

            // Returnér den færdige tekst
            return tekst;
        }

    }
}
