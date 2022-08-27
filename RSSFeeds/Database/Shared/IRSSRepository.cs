using RSSFeeds.Database.Models;

namespace RSSFeeds.Database.Shared
{
    public interface IRSSRepository :IRepository<RSS>
    {
        List<RSS> GetUserFeed(string userId);
        bool CkeckIfExist(string userId, string Link);
    }
}
