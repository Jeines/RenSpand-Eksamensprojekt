using MailKit.Net.Smtp;
using MimeKit;

namespace RenspandWebsite.Service
{
    public class EmailServicecs
    {
        //TODO Skal lige kigges på efter vi har fået sat programmet op til databasen/simply
        public void SendConfirmationEmail(string email, string name)
        {
           // Console.WriteLine(email);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Renspand", "renspandprojekt@gmail.com")); //skal havede lavet en mail til Renspand og insættes
            message.To.Add(new MailboxAddress(name, email)); // modtageren af mailen 
            message.Subject = "OrderBekræftelse - Renspand";


            message.Body = new TextPart("plain")
            {
                Text = $"Hej {name},\n\nTak for din bestilling hos Renspand. Din ordre er blevet modtaget og vil blive behandlet snarest.\n\nMed venlig hilsen,\nRenspand Teamet"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("renspandprojekt@gmail.com ", "wmxpagzpcoevcshd"); // skal havede lavet en mail til Renspand og insættes
                client.Send(message);
                client.Disconnect(true);

            }
            //Console.WriteLine("er sendt");
        }
    }
}


