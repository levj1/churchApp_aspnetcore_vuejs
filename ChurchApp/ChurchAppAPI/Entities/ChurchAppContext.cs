using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Entities
{
    public class ChurchAppContext: DbContext
    {
        public ChurchAppContext(DbContextOptions<ChurchAppContext> options) 
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}
