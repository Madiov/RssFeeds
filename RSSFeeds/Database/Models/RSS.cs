


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSSFeeds.Database.Models
{
    public class RSS
    {
        [Key]
        public long Id { get; set; }
        public string? LinkId { get; set; }
        public string? Title { get; set; }
        public string? Desc { get; set; }
        public string? Link { get; set; }
        public string BaseUrl { get; set; }
        public string? PublishDate { get; set; }
        public string UserID { get; set; }
        public bool isRead { get; set; }
        public bool isBookmarked { get; set; }
    }
}
