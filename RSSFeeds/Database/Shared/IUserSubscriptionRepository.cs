using RSSFeeds.Database.Models;

namespace RSSFeeds.Database.Shared
{
    public interface IUserSubscriptionRepository :IRepository<UserSubscription>
    {
        List<UserSubscription> GetUserSubscription(string userid);
    }
}
