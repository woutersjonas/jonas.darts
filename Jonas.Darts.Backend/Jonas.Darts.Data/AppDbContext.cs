using Jonas.Darts.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Jonas.Darts.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<User> Users => Set<User>();
    }
}
