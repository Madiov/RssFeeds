using System.Linq.Expressions;

namespace RSSFeeds.Database.Shared
{
    public interface IRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void AddRange(IEnumerable<T> entities);
        IQueryable<T> Query(Expression<Func<T, bool>> predicate);
        IQueryable<T> AsQueryable();
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
