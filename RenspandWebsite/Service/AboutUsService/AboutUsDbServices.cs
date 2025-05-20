using Microsoft.EntityFrameworkCore;
using RenSpand_Eksamensprojekt;
using RenspandWebsite.EFDbContext;

namespace RenspandWebsite.Service.AboutUsService
{
    public class AboutUsDbServices : DbService<AboutUs>
    {

        public async Task<AboutUs> GetAboutUsByIdAsync(int id)
        {
            using var context = new RenSpandDbContext();
            //return await context.Profiles
            //        .Include(p => p.Address)
            //        .FirstOrDefaultAsync(p => p.Id == id);

            return await context.AboutUss
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        //public async Task<AboutUs> UpdateAboutUsAsync(AboutUs aboutUs)
        //{
        //    using var context = new RenSpandDbContext();
        //    var existingAboutUs = await context.AboutUss
        //        .FirstOrDefaultAsync(a => a.Id == aboutUs.Id);
        //    if (existingAboutUs != null)
        //    {
        //        existingAboutUs.Titel = aboutUs.Titel;
        //        existingAboutUs.Content = aboutUs.Content;
        //        //existingAboutUs.ImageUrl = aboutUs.ImageUrl;
        //        context.AboutUss.Update(existingAboutUs);
        //        await context.SaveChangesAsync();
        //    }
        //    return existingAboutUs;
        //}
    }
        
}
