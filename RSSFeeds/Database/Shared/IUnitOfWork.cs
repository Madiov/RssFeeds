namespace RSSFeeds.Database.Shared
{
    public interface IUnitOfWork
    {
        IRSSRepository RSS { get; }
        IUserRepository User { get; }
        IUserSubscriptionRepository UserSubscription { get; }
        IRSSCommentRepository RSSComment { get; }
        int Save();
        Task<int> SaveAsync();
        void Dispose();
    }
}
