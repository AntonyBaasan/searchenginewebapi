using System.Linq;
using System.Collections.Generic;
using ElasticSearchEngineService.Models;
using Microsoft.AspNetCore.Mvc;
using SearchEngineDataAccess;
using SearchEngineDomain.Interfaces;

namespace SolrWebService.Controllers
{
    [Produces("application/json")]
    [Route("api/elasticsearch")]
    public class ElasticController : Controller
    {
        private readonly ISearchEngineService<ElasticFileInfo> _searchEngineService;
        private readonly ProphixDBContext _context;

        public ElasticController(ISearchEngineService<ElasticFileInfo> searchEngineService, ProphixDBContext context)
        {
            _searchEngineService = searchEngineService;
            _context = context;
        }

        [HttpGet]
        [Route("index")]
        public string Index()
        {
            var files = _context.FileInfos;
            var fileList = files.ToList();
            var elasticFileInfo = fileList.ConvertAll(f => new ElasticFileInfo
            {
                Id = f.Id,
                ParentId = f.ParentId,
                Name = f.Name,
                Type = f.Type,
                Description = f.Description,
                CreatedBy = f.CreatedBy,
                CreatedOn = f.CreatedOn,
                LastModified = f.LastModified,
            });

            return $"{_searchEngineService.Index(elasticFileInfo)} files indexed";
        }

        [HttpGet]
        [Route("search")]
        public List<ElasticFileInfo> Search(string q)
        {
            return _searchEngineService.Search(q);
        }

        [HttpGet]
        [Route("deleteindex")]
        public string DeleteIndex()
        {
            _searchEngineService.DeleteIndex();
            return "done";
        }

        [HttpGet]
        [Route("createindex")]
        public string CreateIndex()
        {
            _searchEngineService.CreateIndex();
            return "done";
        }
    }
}