using ElasticSearchEngineService;
using Xunit;

namespace SearchEngineTests
{
    public class ElasticTests
    {
        private ElasticServiceImpl service;

        public ElasticTests()
        {
            service = new ElasticServiceImpl(SettingsUtils.GetConnectionString("ElasticSearchEngine"));
        }

        [Fact]
        public void IndexMockTest()
        {
            service.Index();
            
            var all = service.GetAll();
            Assert.Single(all);
        }
        
        [Fact]
        public void QueryByString()
        {
            var res = service.Search("what");
            
            Assert.Single(res);
        }

        [Fact]
        public void GetAllTest()
        {
            var all = service.GetAll();
            System.Console.WriteLine(all.Count);
        }

        [Fact]
        public void DeleteAllTest()
        {
            service.ClearAll();
            
            var all = service.GetAll();
            Assert.Empty(all);
        }

    }
}