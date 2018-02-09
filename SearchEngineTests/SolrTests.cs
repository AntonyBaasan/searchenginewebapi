using SearchEngineDomain.Data;
using SolrSearchEngineService;
using Xunit;

namespace SearchEngineTests
{
    public class SolrTests
    {
        private SolrServiceImpl solr;

        public SolrTests()
        {
            var solrConnectionString = "http://192.168.99.100:8983/solr/collection2";
            SolrNet.Startup.Init<SolrFileInfo>(solrConnectionString);

            solr = new SolrServiceImpl();
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
            
            Assert.Equal(1, res.Count);
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
            Assert.Equal(0, all.Count);
        }
    }
}