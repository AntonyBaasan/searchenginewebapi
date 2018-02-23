using System.Collections.Generic;
using System.Threading.Tasks;
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
            var builder = modelBuilder.Entity<FileInfo>().ToTable("FileSystem");
        }

        public Task<List<FileInfo>> GetAllFiles()
        {
            return FileInfos.FromSql("GetAllFiles").ToListAsync();
        }

    }
}
