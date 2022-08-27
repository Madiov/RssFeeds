namespace RSSFeeds.Services.DTO
{
    public class RSSDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string LinkId { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public List<string> Comments { get; set; } = new List<string>();
        public bool isReaded { get; set; }
        public bool isBookmarked { get; set; }
    }
}
