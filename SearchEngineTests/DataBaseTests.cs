using Microsoft.EntityFrameworkCore;
using SearchEngineDataAccess;
using Xunit;

namespace SearchEngineTests
{
    public class DataBaseTests
    {
        ProphixDBContext dbContext;
        public DataBaseTests()
        {
            var options = new DbContextOptionsBuilder<ProphixDBContext>()
                .UseSqlServer(SettingsUtils.GetConnectionString("ProphixMetaDatabase"))
                .Options;
            dbContext = new ProphixDBContext(options);
        }

        [Fact]
        public async void GetAllFilesTest()
        {
            var allFiles = await dbContext.FileInfos.ToListAsync();
            Assert.Equal(61, allFiles.Count);
        }

        [Fact]
        public async void GetAllFilesByStoredProcedureTest()
        {
            var allFiles = await dbContext.GetAllFiles();
            Assert.Equal(61, allFiles.Count);
        }

         [Fact]
        public async void GetAllFilesTest2()
        {
            var allFiles = await dbContext.FileInfos.ToListAsync();
            Assert.Equal(61, allFiles.Count);
        }
    }
}
