using System.Collections.Generic;
using ElasticSearchEngineService.Models;
using Microsoft.AspNetCore.Mvc;
using SearchEngineDomain.Interfaces;

namespace SolrWebService.Controllers
{
    [Produces("application/json")]
    [Route("api/elasticsearch")]
    public class ElasticController : Controller
    {
        private readonly ISearchEngineService<ElasticFileInfo> _searchEngineService;

        public ElasticController(ISearchEngineService<ElasticFileInfo> searchEngineService)
        {
            _searchEngineService = searchEngineService;
        }

        [HttpGet]
        [Route("search")]
        public List<ElasticFileInfo> Search(string q)
        {
            return _searchEngineService.Search(q);
        }
    }
}