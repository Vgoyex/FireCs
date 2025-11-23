using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Fire.Domain.Enums;

namespace Fire.Domain.Entities
{
    [Table("temporary_posts")]
    public class TemporaryPostsEntity : StandardEntity
    {
        public Guid id { get; set; }
        public Guid user_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string body { get; set; }
        public Privacy privacy { get; set; }
    }
}
