using System;
using System.Linq;
using System.Collections.Generic;
using ElasticSearchEngineService.Models;
using Nest;
using SearchEngineDomain.Interfaces;

namespace ElasticSearchEngineService
{
    public class ElasticServiceImpl<T> : ISearchEngineService<T> where T : ElasticFileInfo
    {
        private ElasticClient client;
        private static string _indexName = "fileinfoindex";

        public ElasticServiceImpl(string connectionString)
        {
            SetupClient(connectionString);
        }

        public ElasticServiceImpl(string connectionString, string indexName)
        {
            _indexName = indexName;
            SetupClient(connectionString);
        }

        private void SetupClient(string connectionString)
        {
            var node = new Uri(connectionString);
            var settings = new ConnectionSettings(node);
            client = new ElasticClient(settings);
        }
        
        public long Index(List<T> docs)
        {
            var files = docs.ConvertAll(d => d);

            var descriptor = new BulkDescriptor();
            files.ForEach(d =>
            {
                descriptor.Index<T>(op => op.Index(_indexName).Document(d));
            });

            var result = client.Bulk(descriptor);

            // return successful indexed document amount
            return result.Items.Where(i=>i.Error==null).Count();
        }

        public List<T> Search(string queryString)
        {
            var response = client.Search<T>(s => s
                    .Index(_indexName)
                    .From(0)
                    .Size(10000)
                    .Query(qry => qry.QueryString(q => q.Query(queryString))));

            return new List<T>(response.Documents);
        }

        public List<T> GetAll()
        {
            var r = client.Search<T>(s => s
                    .Index(_indexName)
                    .From(0)
                    .Size(10000)
                    .Query(q => q.MatchAll()));

            var l = new List<T>(r.Documents);

            return l;
        }

        // NOTE: has to be long!!!
        public long DeleteAll()
        {
            var response = client.DeleteByQuery<T>(d => d
                .Index(_indexName)
                .Query(q => q.MatchAll()));

            return response.Total;
        }

        public long DeleteByIds(List<long> ids)
        {
            throw new NotImplementedException();
        }

        public void DeleteIndex()
        {
            client.DeleteIndex(_indexName);
        }

        public void CreateIndex()
        {
            var createIndexResponse = client.CreateIndex(_indexName, c => c
                .Mappings(ms => ms
                    .Map<ElasticFileInfo>(m => m.AutoMap())
                )
            );
        }
    }
}