using RSSFeeds.Database.Models;

namespace RSSFeeds.Database.Shared
{
    public interface IRSSCommentRepository : IRepository<RSSComment>
    {
        List<RSSComment> GetRssComments(string link);
    }
}
