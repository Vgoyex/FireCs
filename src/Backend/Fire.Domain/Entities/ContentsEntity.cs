namespace Fire.Domain.Entities
{
    public class ContentsEntity : StandardEntity
    {
        public Guid id { get; set; }
        public Guid post_id { get; set; }
        public string file_name { get; set; } = "";
        public string type { get; set; } = "";
        public byte[] file { get; set; } = {};
        public string base64 { get; set; } = "";
        public string storage_path { get; set; } = "";
        public int size { get; set; } = 0;
    }
}
