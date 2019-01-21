using Microsoft.EntityFrameworkCore;
using TTMMC.Models.DBModels;

namespace TTMMC.Services
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Material> Materials { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Mould> Moulds { get; set; }
        public DbSet<Layout> Layouts { get; set; }
        public DbSet<MixtureItem> MixtureItems { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<Mixture> Mixtures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static void Initialize(DBContext context)
        {
            context.Database.EnsureCreatedAsync();
        }
    }
}
