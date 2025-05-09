using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.Service;


namespace RenspandWebsite.Pages.WorkHandler
{
    /// <summary>
    /// This Seed page is used to create a Jason File.
    /// </summary>
    public class SeedModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly JsonFileService<Work> _jsonService;

        public SeedModel(IWebHostEnvironment env)
        {
            _env = env;
            _jsonService = new JsonFileService<Work>(_env);
        }

        public string Message { get; set; }

        public void OnPost()
        {
            var sampleWorks = new List<Work>
            {
                new Work(1, "Container Rens", "Rensning af 1 affaldscontainer – basis løsning", 299),
                new Work(2, "Udvidet Container Rens", "Rensning af 2 containere – standard løsning med rabat", 499),
                new Work(3, "Premium Container Service", "Rensning af 3 containere med ekstra pleje og desinficering", 699),
                new Work(4, "Engangs Rens", "Én enkelt rensning af container uden abonnement", 199),
                new Work(5, "Abonnement – Månedlig", "Fast månedlig rensning af én container", 249),
                new Work(6, "Abonnement – Kvartalsvis", "Kvartalsvis rensning af én container", 599),
                new Work(7, "Ekstra Service", "Tilvalg: indvendig desinficering og lugtfjernelse", 149),
            };

            _jsonService.SaveJsonObjects(sampleWorks);
            Message = "Sample data successfully written to Works.json!";
        }
    }
}
