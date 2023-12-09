using Fortunes.Models;
using Microsoft.EntityFrameworkCore;

namespace Fortunes.DataAccess
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
                
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
