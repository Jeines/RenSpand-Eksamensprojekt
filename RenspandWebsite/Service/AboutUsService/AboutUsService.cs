using RenspandWebsite.EFDbContext;
using RenspandWebsite.Models;
using System;
using RenspandWebsite.Service.AboutUsService;

namespace RenspandWebsite.Service.AboutServices
{
    /// <summary>
    /// Serviceklasse til håndtering af AboutUs-relaterede operationer.
    /// </summary>
    public class AboutUsService
    {
        private readonly AboutUsDbServices _dbService;

        /// <summary>
        /// Initialiserer en ny instans af AboutUsService med den angivne database-service.
        /// </summary>
        /// <param name="context">Database-service til AboutUs-objekter.</param>
        public AboutUsService(AboutUsDbServices context)
        {
            _dbService = context;
        }

        /// <summary>
        /// Henter et AboutUs-objekt ud fra dets ID.
        /// </summary>
        /// <param name="id">ID for det ønskede AboutUs-objekt.</param>
        /// <returns>Det fundne AboutUs-objekt eller null hvis det ikke findes.</returns>
        public AboutUs GetAboutUs(int id)
        {
            return _dbService.GetObjectByIdAsync(id).Result;
        }

        /// <summary>
        /// Opdaterer et eksisterende AboutUs-objekt med nye værdier.
        /// </summary>
        /// <param name="id">ID for det AboutUs-objekt, der skal opdateres.</param>
        /// <param name="aboutUs">Objekt med de nye værdier.</param>
        public void UpdateAboutUS(int id, AboutUs aboutUs)
        {
            var existingAboutUs = _dbService.GetObjectByIdAsync(id).Result;
            if (existingAboutUs != null)
            {
                existingAboutUs.Titel = aboutUs.Titel;
                existingAboutUs.Content = aboutUs.Content;
                //_dbService.UpdateAboutUsAsync(aboutUs).Wait();
                //existingAboutUs.ImageUrl = aboutUs.ImageUrl;
                _dbService.UpdateObjectAsync(existingAboutUs).Wait();
            }
            //else
            //{
            //    throw new Exception("AboutUs not found");
            //}
        }
    }
}
