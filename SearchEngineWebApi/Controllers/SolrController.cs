using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SearchEngineDomain;

namespace SolrWebService.Controllers
{
    [Produces("application/json")]
    [Route("api/solr")]
    public class SolrController : Controller
    {
        private readonly ISearchEngineService _searchEngineService;

        public SolrController(ISearchEngineService searchEngineService)
        {
            _searchEngineService = searchEngineService;
        }

        // GET api/Indexing
        [HttpGet]
        [Route("indexing")]
        public string Indexing()
        {
            int numOfAffected = _searchEngineService.Index();
            return $"{numOfAffected} rows indexed";
        }

        [HttpGet]
        [Route("search")]
        public IEnumerable<object> Search(string q)
        {
            return _searchEngineService.Search(q);
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<object> All()
        {
            return _searchEngineService.Search("*:*");
        }

        // GET api/clear
        [HttpGet]
        [Route("clear")]
        public string Clear()
        {
            int numOfAffected = _searchEngineService.ClearAll();
            return $"{numOfAffected} rows removed";
        }
    }
}