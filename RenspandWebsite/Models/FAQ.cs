namespace RenspandWebsite.Models
{
    /// <summary>
    /// Repræsenterer en FAQ (Ofte Stillede Spørgsmål) med et spørgsmål og et svar.
    /// </summary>
    public class FAQ
    {
        /// <summary>
        /// Propertys til at repræsentere attributterne i FAQ.
        /// </summary>
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        /// <summary>
        /// Konstruktør til at initialisere en FAQ instans med id, spørgsmål og svar.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="question"></param>
        /// <param name="answer"></param>
        public FAQ(int id, string question, string answer)
        {
            Id = id;
            Question = question;
            Answer = answer;
        }
        /// <summary>
        /// Default konstruktør til at initialisere en FAQ instans.
        /// </summary>
        public FAQ() { }

        /// <summary>
        /// Returnerer en strengrepræsentation af FAQ'en, der inkluderer id, spørgsmål og svar.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Id: {Id}, Spørgsmål: {Question}, Svar: {Answer}";
        }
}
