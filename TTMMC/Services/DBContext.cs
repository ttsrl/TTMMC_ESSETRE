using Microsoft.EntityFrameworkCore;
using TTMMC.Models.DBModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TTMMC.Services
{
    public class DBContext : DbContext
    {
        public static DbContextOptions<DBContext> Options;

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            Options = options;
        }

        public DbSet<Material> Materials { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Mould> Moulds { get; set; }
        public DbSet<Layout> Layouts { get; set; }
        public DbSet<LayoutRecord> LayoutsActRecords { get; set; }
        public DbSet<LayoutRecordField> LayoutsActRecordsFields { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<Mixture> Mixtures { get; set; }
        public DbSet<MixtureItem> MixtureItems { get; set; }

        private static DBContext _instance;
        public static DBContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DBContext(Options);
                }

                return _instance;
            }
        }

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
