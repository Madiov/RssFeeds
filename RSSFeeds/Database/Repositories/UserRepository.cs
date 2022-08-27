using RSSFeeds.Database.Models;
using RSSFeeds.Database.Shared;

namespace RSSFeeds.Database.Repositories
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        public UserRepository(RSSFeedContext context) : base(context)
        {
        }
    }
}
