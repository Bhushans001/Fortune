using Microsoft.EntityFrameworkCore;

namespace Fortune.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
                
        }
    }
}
