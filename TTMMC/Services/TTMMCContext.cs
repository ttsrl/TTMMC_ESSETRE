using Microsoft.EntityFrameworkCore;
using TTMMC_ESSETRE.Models.DBModels;

namespace TTMMC_ESSETRE.Services
{
    public class TTMMCContext : DbContext
    {
        public static DbContextOptions<TTMMCContext> Options;

        public TTMMCContext(DbContextOptions<TTMMCContext> options) : base(options)
        {
            Options = options;
        }

        public DbSet<Layout> Layouts { get; set; }
        public DbSet<LayoutRecord> LayoutsActRecords { get; set; }
        public DbSet<LayoutRecordField> LayoutsActRecordsFields { get; set; }

        private static TTMMCContext _instance;
        public static TTMMCContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TTMMCContext(Options);
                }

                return _instance;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static void Initialize(TTMMCContext context)
        {
            context.Database.EnsureCreatedAsync();
        }
    }
}
