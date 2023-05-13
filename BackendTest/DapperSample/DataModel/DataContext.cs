using Microsoft.EntityFrameworkCore;

namespace STGenetics.DataModel
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { 
        
        }

        public DbSet<AnimalModel> Animal { get; set; }
    }
}
