using RSSFeeds.Database.Models;
using RSSFeeds.Database.Shared;

namespace RSSFeeds.Database.Repositories
{
    public class RSSRepository : Repository<RSS> ,IRSSRepository
    {
        public RSSRepository(RSSFeedContext context) : base(context)
        {
        }
        public List<RSS> GetUserFeed(string userId)
        {
            return  AsQueryable().Where(u=>u.UserID==userId).ToList();
        }
        public bool CkeckIfExist(string userId,string LinkId)
        {
            var Rss = AsQueryable().Where(r=>r.UserID==userId && r.LinkId==LinkId).FirstOrDefault();
            if (Rss != null)
                return true;
            return false;
        }

    }
}
