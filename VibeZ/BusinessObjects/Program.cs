using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects
{
    public class VibeZDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }
        public virtual DbSet<BlockedArtist> BlockedArtists { get; set; }
        public virtual DbSet<Library> Libraries { get; set; }
        //public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.
                GetConnectionString("VibeZDB"));


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
