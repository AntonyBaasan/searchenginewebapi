namespace ElasticSearchEngineService.Models
{
    public class ElasticFileInfo
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string LastModified { get; set; }
    }
}