using Microsoft.EntityFrameworkCore;

namespace SearchEngineDataAccess
{
    public class ProphixDBContext : DbContext
    {
        public ProphixDBContext(DbContextOptions<ProphixDBContext> options) : base(options)
        {
        }

        public DbSet<FileInfo> FileInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileInfo>().ToTable("FileSystem");
        }
    }
}
