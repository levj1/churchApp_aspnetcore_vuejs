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
    }
}
