using Microsoft.EntityFrameworkCore;
using RSSFeeds.Database.Shared;
using System.Linq.Expressions;

namespace RSSFeeds.Database.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly RSSFeedContext _context;

        public Repository(RSSFeedContext context)
        {
            _context = context;
        }
        public T Get(Expression<Func<T, bool>> predicate) => _context.Set<T>().FirstOrDefault(predicate);
        public IEnumerable<T> GetAll() => _context.Set<T>().ToList();
        public void Add(T entity) => _context.Entry<T>(entity).State = EntityState.Added;
        public void Remove(T entity) => _context.Entry<T>(entity).State = EntityState.Deleted;
        public void Update(T entity) => _context.Entry<T>(entity).State = EntityState.Modified;
        public void AddRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                _context.Entry<T>(entity).State = EntityState.Added;
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                _context.Entry<T>(entity).State = EntityState.Deleted;
        }
        public IQueryable<T> AsQueryable() => _context.Set<T>().AsQueryable<T>();
        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).AsQueryable();
        }
        public DbContext GetContext()
        {
            return _context;
        }
    }
}
