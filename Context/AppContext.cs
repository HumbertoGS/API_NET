using API_en_NET.Models;
using Microsoft.EntityFrameworkCore;

namespace API_en_NET.Context
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options): base(options) 
        { 

        }

        public DbSet<Person> Persons { get; set; }
    }
}
