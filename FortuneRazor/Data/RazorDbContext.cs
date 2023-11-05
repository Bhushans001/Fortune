using FortuneRazor.Models;
using Microsoft.EntityFrameworkCore;

namespace FortuneRazor.Data
{
    public class RazorDbContext : DbContext
    {
        public RazorDbContext(DbContextOptions<RazorDbContext> options) : base(options)
        {
                
        }
        public DbSet<Category> Category { get; set; }   
    }
}
