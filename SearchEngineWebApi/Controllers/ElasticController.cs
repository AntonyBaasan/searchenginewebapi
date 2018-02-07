using Microsoft.AspNetCore.Mvc;
using SearchEngineDomain;

namespace SolrWebService.Controllers
{
    [Produces("application/json")]
    [Route("api/elasticsearch")]
    public class ElasticController : Controller
    {
        private readonly ISearchEngineService _searchEngineService;

        public ElasticController(ISearchEngineService searchEngineService)
        {
            _searchEngineService = searchEngineService;
        }

        [HttpGet]
        public string Get(string q)
        {
            return q;
        }
    }
}