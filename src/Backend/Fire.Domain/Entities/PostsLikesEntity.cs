using System.ComponentModel.DataAnnotations.Schema;

namespace Fire.Domain.Entities
{
    public class PostsLikesEntity
    {
        public Guid id { get; set; }
        public Guid user_id { get; set; }
        public Guid post_id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime created_at { get; set; } = DateTime.UtcNow;

    }
}
