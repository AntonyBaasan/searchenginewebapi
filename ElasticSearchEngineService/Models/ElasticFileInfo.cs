using System;
using Nest;

namespace ElasticSearchEngineService.Models
{
    public class ElasticFileInfo
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int Type { get; set; }
        [Text(Analyzer = "simple")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModified { get; set; }
    }
}