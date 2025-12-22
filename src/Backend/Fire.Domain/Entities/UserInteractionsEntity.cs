namespace Fire.Domain.Entities
{
    public class UserInteractionsEntity
    {
        public Guid id { get; set; }
        public Guid user_id { get; set; }
        public Guid author_id { get; set; }
        public int count { get; set; }
        public DateTime updated_at { get; set; } = DateTime.UtcNow;

    }
}
