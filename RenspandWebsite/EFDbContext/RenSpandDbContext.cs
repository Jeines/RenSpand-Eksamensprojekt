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
        /// using Fluent API to configure the relationship between AddressItem and Order
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

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressItem> AddressItems { get; set; }
        public DbSet<AboutUs> AboutUss { get; set; }




    }
}
