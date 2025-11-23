using System.ComponentModel.DataAnnotations.Schema;

namespace Fire.Domain.Entities
{
    [Table("users")]
    public class UsersEntity : StandardEntity
    {
        public Guid  id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string nickname { get; set; }
        public string role { get; set; } = "0";
    }
}
