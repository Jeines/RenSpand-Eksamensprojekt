using RenspandWebsite.EFDbContext;
using RenSpand_Eksamensprojekt;
using System;
using RenspandWebsite.Service.AboutUsService;

namespace RenspandWebsite.Service.AboutServices
{
    public class AboutUsService 
    {
        private readonly AboutUsDbServices _dbService;

        public AboutUsService(AboutUsDbServices context)
        {
            _dbService = context;
        }

       

        
        public AboutUs GetAboutUs(int id)
        {
            return _dbService.GetObjectByIdAsync(id).Result;
            
        }

        public void UpdateAboutUS(int id, AboutUs aboutUs)
        {

            // Opdaterer felterne i den eksisterende AboutUs
            //_dbService.UpdateAboutUsAsync(aboutUs).Wait();
            var existingAboutUs = _dbService.GetObjectByIdAsync(id).Result;
            if (existingAboutUs != null)
            {
                existingAboutUs.Titel = aboutUs.Titel;
                existingAboutUs.Content = aboutUs.Content;
                //existingAboutUs.ImageUrl = aboutUs.ImageUrl;
                _dbService.UpdateObjectAsync(existingAboutUs).Wait();
            }
            //else
            //{
            //    throw new Exception("AboutUs not found");
            //}
        }

        //public void UpdateAbout(int id, string title, string content)
        //{
        //    var about = _dbService.AboutUs.FirstOrDefault(a => a.Id == id);
        //    if (about != null)
        //    {
        //        about.Titel = title;
        //        about.Content = content;
        //        _dbService.SaveChanges();
        //    }
        //}
    }
}
