using Microsoft.EntityFrameworkCore;
using RSSFeeds.Database.Repositories;
using RSSFeeds.Database.Shared;

namespace RSSFeeds.Database
{

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly object _object = new object();
        private RSSFeedContext _context = null;
        private readonly Type ContextType;
        public UnitOfWork(DbContextOptions options)
        {
            _context = new RSSFeedContext(options);
            ContextType = typeof(RSSFeedContext);
        }

        public IRSSRepository RSS => new RSSRepository(_context);
        public IUserRepository User => new UserRepository(_context);
        public IRSSCommentRepository RSSComment => new RSSCommentRepository(_context);
        public IUserSubscriptionRepository UserSubscription => new UserSubscriptionRepository(_context);

        public int Save()
        {
            lock (_object)
            {
                return _context.SaveChanges();
            }
        }
        public async Task<int> SaveAsync()
        {

            return await _context.SaveChangesAsync();


        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
