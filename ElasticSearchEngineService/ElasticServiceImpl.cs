using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ElasticSearchEngineService.Models;
using Nest;
using SearchEngineDomain;

namespace ElasticSearchEngineService
{
    public class ElasticServiceImpl: ISearchEngineService
    {
        private ElasticClient client;

        public ElasticServiceImpl(string connectionString)
        {
            var node = new Uri(connectionString);
            var settings = new ConnectionSettings(node);
            client = new ElasticClient(settings); 
        }
        
        public int Index()
        {
            var file = new ElasticFileInfo
            {
                Id = 1,
                Name = "kimchy",
                CreatedOn = (new DateTime(2009, 11, 15)).ToString(CultureInfo.InvariantCulture),
                CreatedBy = "Trying out NEST, so far so good?"
            };
            
            var response = client.Index(file, idx => idx.Index("fileinfoindex"));
            
            return 1;
        }

        public List<object> Search(string q)
        {
            throw new System.NotImplementedException();
        }

        public List<object> GetAll()
        {
            var r = client.Get<ElasticFileInfo>(1, idx=>idx.Index("fileinfoindex"));
            var l = new List<object>();
            l.Add(r.Source);

            return l;
            
//            List<object> indexedList = new List<object>();
//            var scanResults = client.Search<ElasticFileInfo>(s => s
//                .From(0)
//                .Size(2000)
//                .MatchAll()
//                .Scroll("5m")
//            );
//
//            var results = client.Scroll<ElasticFileInfo>("10m", scanResults.ScrollId);
//
//            return indexedList;
        }

        public int ClearAll()
        {
            throw new System.NotImplementedException();
        }
    }
}