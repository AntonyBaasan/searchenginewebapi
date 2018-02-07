using System.Collections.Generic;
using System.Threading.Tasks;
using CommonServiceLocator;
using SearchEngineDomain;
using SearchEngineDomain.Data;
using SolrNet;

namespace SolrService
{
    public class SolrServiceImpl : ISearchEngineService
    {
        public SolrServiceImpl()
        {

        }

        public int Index()
        {
            var docs = GetMockData();

            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<SolrDocument>>();
            solr.AddRange(docs);
            ResponseHeader response = solr.Commit();
            if (response.Status == 0)//OK
                return docs.Count;

            throw new System.Exception("Can't index");
        }

        public int ClearAll()
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<SolrDocument>>();
            ResponseHeader r = solr.Delete(SolrQuery.All);
            ResponseHeader response = solr.Commit();

            if (response.Status == 0)
                return 1;

            throw new System.Exception("Can't remove");
        }

        public List<object> Search(string q)
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<SolrDocument>>();
            var results = solr.Query(new SolrQueryByField("name", q) { Quoted = false } + new SolrQueryByField("desc", q) { Quoted = false });
            return new List<object> { results };
        }

        private List<SolrDocument> GetMockData()
        {
            return new List<SolrDocument> {
                new SolrDocument{Id=1, Name = new List<string> {"doc1"}, Description=new List<string> {"THis is a document1111" } },
                new SolrDocument{Id=2, Name= new List<string> {"doc2" }, Description=new List<string> {"THis is a second document"} },
                new SolrDocument{Id=3, Name= new List<string> {"doc2" }, Description= new List<string> {"THis is a third document"} },
                new SolrDocument{Id=4, Name= new List<string> {"document4" }, Description= new List<string> {"Yo my friend"} },
                new SolrDocument{Id=5, Name= new List<string> {"document5" }, Description= new List<string> {"what to play a game"} },
                new SolrDocument{Id=6, Name= new List<string> {"document6" }, Description= new List<string> {"hi there, how are you doing?" } }
            };
        }

    }
}