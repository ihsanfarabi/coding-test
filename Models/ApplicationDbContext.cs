using Microsoft.EntityFrameworkCore;

namespace iaccess_test.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<SearchString> SearchString { get; set; }
    }
}
