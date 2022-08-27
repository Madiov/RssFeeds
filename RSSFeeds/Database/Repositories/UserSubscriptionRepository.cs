using RSSFeeds.Database.Models;
using RSSFeeds.Database.Shared;

namespace RSSFeeds.Database.Repositories
{
    public class UserSubscriptionRepository :Repository<UserSubscription> , IUserSubscriptionRepository
    {
        public UserSubscriptionRepository(RSSFeedContext context) : base(context)
        {
        }
        public List<UserSubscription> GetUserSubscription(string userid)
        {
            return AsQueryable().Where(us => us.UserId == userid).ToList();
        }
    }
}
