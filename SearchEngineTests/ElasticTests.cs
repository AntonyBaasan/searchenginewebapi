using ElasticSearchEngineService;
using SearchEngineDomain.Models;
using SolrSearchEngineService;
using Xunit;

namespace SearchEngineTests
{
    public class ElasticTests
    {
        private ElasticServiceImpl service;

        public ElasticTests()
        {
            const string connectionString = "http://192.168.99.100:9200";
            service = new ElasticServiceImpl(connectionString);
        }

        [Fact]
        public void IndexMockTest()
        {
            service.Index();
            
            var all = service.GetAll();
            Assert.Equal(1, all.Count);
        }
        
        [Fact]
        public void QueryByString()
        {
            var res = service.Search("what");
            
            Assert.Equal(1, res.Count);
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
            Assert.Equal(0, all.Count);
        }

    }
}