using Microsoft.EntityFrameworkCore;

namespace EasyRabbit.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }

        public DbSet<Calculation> Calculations { get; set; }
    }
}
