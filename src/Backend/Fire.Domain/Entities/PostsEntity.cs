using System.ComponentModel.DataAnnotations.Schema;

namespace Fire.Domain.Entities
{
    [Table("posts")]
    public class PostsEntity : StandardEntity
    {
        public Guid id { get; set; }
        public Guid user_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string body { get; set; }
        public string privacy { get; set; } = "PRIVATE";
        public List<string> list_contents { get; set; } = new List<string>();
    }
}
