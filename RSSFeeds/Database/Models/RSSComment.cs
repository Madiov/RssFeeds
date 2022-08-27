namespace RSSFeeds.Database.Models
{
    public class RSSComment
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Link { get; set; }
        public string Comment{  get; set;}
    }
}
