namespace RSSFeeds.Database.Models
{
    public class UserSubscription
    {
        public long id { get; set; }
        public string UserId { get; set; }
        public string RSSURL { get; set; }
    }
}
