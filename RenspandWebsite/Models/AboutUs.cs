namespace RenspandWebsite.Models
{
    /// <summary>
    /// Repræsenterer en "About Us"-side med information om virksomheden.
    /// </summary>
    public class AboutUs
    {
        /// <summary>
        /// Propertys til at repræsentere attributterne i AboutUs.
        /// </summary>
        public int Id { get; set; }
        public string Titel { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        //public string? ImageUrl { get; set; } = string.Empty;

        public AboutUs()
        {

        }
        /// <summary>
        /// Konstruktør til at initialisere en AboutUs instans med titel og indhold.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public AboutUs(string title, string content)
        {
            Titel = title;
            Content = content;
        }

        /// <summary>
        /// Overrider ToString metoden for at returnere en streng repræsentation af AboutUs objektet.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"ID: {Id}, Titel {Titel}, Content {Content}";
        }
    }
}
