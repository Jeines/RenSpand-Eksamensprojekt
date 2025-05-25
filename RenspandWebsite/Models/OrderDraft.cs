namespace RenspandWebsite.Models
{
    /// <summary>
    /// Repræsenterer en ordreudkast, der indeholder oplysninger om køberen, serviceartikler og adresser.
    /// Udkastet bruges til at indsamle oplysninger fra brugeren, før ordren sendes.
    /// Bliver brugt i OrderServicePageModel.
    /// </summary>
    public class OrderDraft
    {
        /// <summary>
        /// Propertys til at repræsentere attributterne i OrderDraft.
        /// </summary>
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public List<int> SelectedWorkIds { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime TrashCanEmptyDate { get; set; }
        public string CustomerNote { get; set; }
    }
}
