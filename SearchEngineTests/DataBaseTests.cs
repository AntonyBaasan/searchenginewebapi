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
                //.UseSqlServer("Data Source=DEVLATE557026\\DEVSRVR;Initial Catalog=PROPHIX_MetadataDb;Integrated Security=True")
                .UseSqlServer("Data Source=192.168.99.100;Initial Catalog=TestDB;Persist Security Info=True;User ID=sa;Password=SuperString321")
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
        public async void GetAllFilesTest2()
        {
            var allFiles = await dbContext.FileInfos.ToListAsync();
            Assert.Equal(61, allFiles.Count);
        }
    }
}
