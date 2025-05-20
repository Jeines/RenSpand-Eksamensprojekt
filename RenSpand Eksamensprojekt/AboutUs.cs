using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenSpand_Eksamensprojekt
{
    public class AboutUs
    {
        
        public int Id { get; set; }
        public string Titel { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        //public string? ImageUrl { get; set; } = string.Empty;

        public AboutUs() 
        {

        }
        public AboutUs(string title, string content)
        {
            Titel = title;
            Content = content;
        }


        public override string ToString()
        {
            return $"ID: {Id}, Titel {Titel}, Content {Content}";
        }
    }



}
