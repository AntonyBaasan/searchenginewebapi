using System.Collections.Generic;
using SolrNet.Attributes;

namespace SearchEngineDomain.Data
{
    public class SolrDocument
    {
        [SolrUniqueKey("id")]
        public int Id { get; set; }
        [SolrField("name")]
        public ICollection<string> Name { get; set; }
        [SolrField("desc")]
        public ICollection<string> Description { get; set; }
    }
}
