namespace RenspandWebsite.Models
{
    /// <summary>
    /// Repræsenterer oplysninger om en virksomhed, herunder navn, kontaktoplysninger og beskrivelse.
    /// </summary>
    public class CompanyInformation
    {
        /// <summary>
        /// Propertys til at repræsentere attributterne i CompanyInformation.
        /// </summary>
        public string CompanyName { get; set; }
        public string MobileNumber { get; set; }
        public string OfficeNumber { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyAddress { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Initialiserer en ny instans af CompanyInformation med angivne værdier.
        /// </summary>
        public void OnGet()
        {
            CompanyName = "Renspand";
            MobileNumber = "+45 31626924";
            OfficeNumber = "+45 81815758";
            CompanyEmail = "anderse@renspand.dk";
            CompanyAddress = "Renspand , Køge, Danmark";
            ImageUrl = "/Assets/RenSpandLogo.png";
            Description = "Renspand er et firma der kører rundt i køge og renser skraldespande hjemme hos folk";
        }
    }
}
