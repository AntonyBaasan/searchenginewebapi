using System.Collections.Generic;
using SolrNet.Attributes;

namespace SearchEngineDomain.Models
{
    public class SolrFileInfo
    {
        [SolrUniqueKey("id")]
        public int Id { get; set; }
        [SolrField("parentid", Boost = 1000)]
        public ICollection<int> ParentId { get; set; }
        [SolrField("name")]
        public ICollection<string> Name { get; set; }
        [SolrField("type")]
        public ICollection<string> Type { get; set; }
        [SolrField("desc")]
        public ICollection<string> Description { get; set; }
        [SolrField("createdby")]
        public ICollection<string> CreatedBy { get; set; }
        [SolrField("createdon")]
        public ICollection<string> CreatedOn { get; set; }
        [SolrField("lastmodified")]
        public ICollection<string> LastModified { get; set; }
    }
}
