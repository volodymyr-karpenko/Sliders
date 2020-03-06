using Microsoft.EntityFrameworkCore;

namespace Sliders.API.Data
{
    public class SlidersWebContext : DbContext
    {
        public SlidersWebContext(DbContextOptions<SlidersWebContext> options)
            : base(options)
        {
        }

        public DbSet<Models.SlidersData> SlidersData { get; set; }
    }
}