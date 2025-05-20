using Microsoft.EntityFrameworkCore;
using RenSpand_Eksamensprojekt;

namespace RenspandWebsite.EFDbContext
{
    public class RenSpandDbContext : DbContext
    {
    
         protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RenSpandDB; Integrated Security=True; Connect Timeout=30; Encrypt=False");
        }

        /// <summary>  
        /// Brug af Fluent API til at konfigurere relationen mellem AddressItem og Order  
        /// </summary>  
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressItem>()
                .HasOne(ai => ai.Order)
                .WithMany()
                .HasForeignKey(ai => ai.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<AboutUs>().HasData(
                new AboutUs
                {
                    Id = 1,
                    Titel = "About Us",
                    Content = "We are a company that cleans trash cans in the Køge area.",
                    //ImageUrl = "/Assets/RenSpandLogo.png"
                }
            );


            // Seed initialisere FAQ data så når databasen bliver oprette vil der altid eksistere en enkelt FAQ som forklare om hvordan du kan kontakte firmaet.  
            modelBuilder.Entity<FAQ>().HasData(
                new FAQ { Id = -1, Question = "Hvordan kontakter jeg virksomhenden?", Answer = "Du kan kontakte os via vores hjemmeside hvor Email og Telefonnummer er angivet." }
            );
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressItem> AddressItems { get; set; }
        public DbSet<AboutUs> AboutUss { get; set; }
        public DbSet<FAQ> FAQs { get; set; }

    }
}
