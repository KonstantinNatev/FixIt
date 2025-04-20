using Microsoft.EntityFrameworkCore;
using FixIt.Models;

namespace FixIt.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<RepairRequest> RepairRequests { get; set; }
    }
}
