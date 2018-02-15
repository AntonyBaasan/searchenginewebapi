using SearchEngineDomain.Models;
using SolrSearchEngineService;
using Xunit;

namespace SearchEngineTests
{
    public class SolrTests
    {
        private SolrServiceImpl solr;

        public SolrTests()
        {
            solr = new SolrServiceImpl(SettingsUtils.GetConnectionString("SolrSearchEngine"));
        }

        [Fact]
        public void IndexMockTest()
        {
            solr.Index();
            
            var all = solr.GetAll();
            Assert.Equal(6, all.Count);
        }
        
        [Fact]
        public void QueryByString()
        {
            var res = solr.Search("what");
            
            Assert.Single(res);
        }

        [Fact]
        public void GetAllTest()
        {
            var all = solr.GetAll();
            System.Console.WriteLine(all.Count);
        }

        [Fact]
        public void DeleteAllTest()
        {
            solr.ClearAll();
            
            var all = solr.GetAll();
            Assert.Empty(all);
        }
    }
}