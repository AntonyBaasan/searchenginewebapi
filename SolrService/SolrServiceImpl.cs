﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CommonServiceLocator;
using SearchEngineDomain;
using SearchEngineDomain.Data;
using SolrNet;

namespace SolrService
{
    public class SolrServiceImpl : ISearchEngineService
    {
        private ISolrOperations<SolrFileInfo> _solrOperations;
        
        public SolrServiceImpl()
        {
            _solrOperations = ServiceLocator.Current.GetInstance<ISolrOperations<SolrFileInfo>>();
        }

        public int Index()
        {
            var docs = GetMockData();

            _solrOperations.AddRange(docs);
            ResponseHeader response = _solrOperations.Commit();
            if (response.Status == 0)//OK
                return docs.Count;

            throw new System.Exception("Can't index");
        }
        
        public int Index(List<SolrFileInfo> docs)
        {
            _solrOperations.AddRange(docs);
            ResponseHeader response = _solrOperations.Commit();
            if (response.Status == 0)//OK
                return docs.Count;

            throw new System.Exception("Can't index");
        }

        public int ClearAll()
        {
            ResponseHeader r = _solrOperations.Delete(SolrQuery.All);
            ResponseHeader response = _solrOperations.Commit();

            if (response.Status == 0)
                return 1;

            throw new System.Exception("Can't remove");
        }

        public List<object> GetAll()
        {
            var results = _solrOperations.Query(SolrQuery.All);
            return new List<object>(results);
        }
        
        public List<object> Search(string q)
        {
            var results = _solrOperations.Query(new SolrQuery(q));
            return new List<object> (results);
        }

        private List<SolrFileInfo> GetMockData()
        {
            return new List<SolrFileInfo> {
                new SolrFileInfo{Id=1, Name = new List<string> {"doc1"}, Description=new List<string> {"THis is a document1111" } },
                new SolrFileInfo{Id=2, Name= new List<string> {"doc2" }, Description=new List<string> {"THis is a second document"} },
                new SolrFileInfo{Id=3, Name= new List<string> {"doc2" }, Description= new List<string> {"THis is a third document"} },
                new SolrFileInfo{Id=4, Name= new List<string> {"document4" }, Description= new List<string> {"Yo my friend"} },
                new SolrFileInfo{Id=5, Name= new List<string> {"document5" }, Description= new List<string> {"what to play a game"} },
                new SolrFileInfo{Id=6, Name= new List<string> {"document6" }, Description= new List<string> {"hi there, how are you doing?" } }
            };
        }

    }
}