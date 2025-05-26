
namespace RenspandWebsite.Models
{
    /// <summary>
    /// Repræsenterer en medarbejderprofil, der arver fra Profile-klassen.
    /// </summary>
    public class Employee : Profile
    {
        /// <summary>
        /// Propertys til at repræsentere attributterne i Employee.
        /// </summary>
        public int YearsOfExperians { get; set; }

        public decimal Salary { get; set; }

        public List<string> Qualifications { get; set; } = new List<string>();
        /// <summary>
        /// Konstruktør til Employee-klassen.
        /// </summary>
        /// <param name="yearsOfExperians"></param>
        /// <param name="salary"></param>
        /// <param name="qualifications"></param>
        public Employee(int yearsOfExperians, decimal salary, List<string> qualifications)
        {
            YearsOfExperians = yearsOfExperians;
            Salary = salary;
            Qualifications = qualifications;
        }

        public Employee() { }

        /// <summary>
        /// Overrider ToString metoden for at returnere en streng repræsentation af Employee objektet.
        /// </summary>
        /// <returns></returns>
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
