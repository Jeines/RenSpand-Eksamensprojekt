using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenSpand_Eksamensprojekt
{
    public class FAQ
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public FAQ(int id, string question, string answer)
        {
            Id = id;
            Question = question;
            Answer = answer;
        }

        public FAQ() { }




    }
}
