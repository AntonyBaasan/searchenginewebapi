using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<string> Index()
        {
            List<FileInfo> files = await _context.GetAllFiles();
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
                TypeAsString = ResolveType(f.Type),
                Location = ResolveLocation(f.Location)
            });

            return $"{_searchEngineService.Index(elasticFileInfo)} files indexed";
        }

        private string ResolveType(int type)
        {
            FileType fileType = (FileType) Enum.Parse(typeof(FileType), type.ToString());
            return SplitCamelCase(fileType.ToString());
        }

        private string ResolveLocation(string str)
        {
            var filePath = str.Trim();
            var userIndex = str.IndexOf("([FSPH*1");
            if (userIndex != 0)
            {
                var folderParse = filePath.Split('\\');
                filePath = filePath.Replace("([FSPH*1", "Users");
            }

            filePath = filePath.Replace("([FSPH*2", "recycle bin");
            filePath = filePath.Replace("([FSPH*0", "public");

            return filePath;
        }

        public static string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
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