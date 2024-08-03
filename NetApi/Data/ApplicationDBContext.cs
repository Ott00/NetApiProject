using Microsoft.EntityFrameworkCore;
using NetApi.Entities;

namespace NetApi.Database
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
