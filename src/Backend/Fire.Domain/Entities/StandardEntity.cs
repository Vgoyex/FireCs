using System.ComponentModel.DataAnnotations.Schema;

namespace Fire.Domain.Entities
{
    public class StandardEntity
    {
        public bool active { get; set; } = true;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime created_at { get; set; } = DateTime.UtcNow;

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
    }
}
