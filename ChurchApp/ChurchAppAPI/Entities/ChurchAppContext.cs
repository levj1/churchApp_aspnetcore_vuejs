using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChurchAppAPI.Entities
{
    public class ChurchAppContext: IdentityDbContext<AppUser>
    {
        public ChurchAppContext(DbContextOptions<ChurchAppContext> options) 
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Donation> Donations { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<AddressType> AddresseTypes { get; set; }

        public DbSet<DonationType> DonationTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Donation>()
                .Property(b => b.DonationCreatedDate)
                .HasDefaultValueSql("getdate()");
        }
    }
}
