using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;


namespace RenspandWebsite.Pages.Work
{
    /// <summary>
    /// This Seed page is used to create a Jason File.
    /// </summary>
    public class SeedModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly JsonFileService<RenSpand_Eksamensprojekt.Work> _jsonService;

        public SeedModel(IWebHostEnvironment env)
        {
            _env = env;
            _jsonService = new JsonFileService<RenSpand_Eksamensprojekt.Work>(_env);
        }

        public string Message { get; set; }

        public void OnPost()
        {
            var sampleWorks = new List<RenSpand_Eksamensprojekt.Work>
            {
                new RenSpand_Eksamensprojekt.Work(1, "Container Rens", "Rensning af 1 affaldscontainer – basis løsning", 299),
                new RenSpand_Eksamensprojekt.Work(2, "Udvidet Container Rens", "Rensning af 2 containere – standard løsning med rabat", 499),
                new RenSpand_Eksamensprojekt.Work(3, "Premium Container Service", "Rensning af 3 containere med ekstra pleje og desinficering", 699),
                new RenSpand_Eksamensprojekt.Work(4, "Engangs Rens", "Én enkelt rensning af container uden abonnement", 199),
                new RenSpand_Eksamensprojekt.Work(5, "Abonnement – Månedlig", "Fast månedlig rensning af én container", 249),
                new RenSpand_Eksamensprojekt.Work(6, "Abonnement – Kvartalsvis", "Kvartalsvis rensning af én container", 599),
                new RenSpand_Eksamensprojekt.Work(7, "Ekstra Service", "Tilvalg: indvendig desinficering og lugtfjernelse", 149),
            };

            _jsonService.SaveJsonObjects(sampleWorks);
            Message = "Sample data successfully written to Works.json!";
        }
    }
}
