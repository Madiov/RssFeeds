using RSSFeeds.Database.Models;
using RSSFeeds.Database.Shared;

namespace RSSFeeds.Database.Repositories
{
    public class RSSCommentRepository : Repository<RSSComment> ,IRSSCommentRepository
    {
        public RSSCommentRepository(RSSFeedContext context) : base(context)
        {
        }
        public List<RSSComment> GetRssComments(string link)
        {
            return AsQueryable().Where(c=>c.Link == link).ToList();
        }
    }
}
