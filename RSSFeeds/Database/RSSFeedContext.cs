using Microsoft.EntityFrameworkCore;
using RSSFeeds.Database.Models;

namespace RSSFeeds.Database
{
    public class RSSFeedContext :DbContext
    {
        public RSSFeedContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<User> users { get; set; }
        public virtual DbSet<RSS> RSS { get; set; }
        public virtual DbSet<RSSComment> RSSComments { get; set; }
        public virtual DbSet<UserSubscription> userSubscriptions { get; set; }
        
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
