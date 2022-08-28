using Fanap.MarginParkingPlus.Services.Shared;
using RSSFeeds.Database.Models;
using RSSFeeds.Database.Shared;
using RSSFeeds.Services.Shared;
using System.ServiceModel.Syndication;
using System.Xml;
using Polly;
using Polly.Retry;

namespace RSSFeeds.Services.Services
{
    public class RSSUpdaterService : BaseService, IRSSUpdaterService
    {
        private readonly AsyncRetryPolicy retryPolicy;
        private readonly int maxRetries = 3;
        int counter = 0;
        public RSSUpdaterService(ILoggerManager logger, IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork, logger, configuration)
        {
            retryPolicy = Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(retryCount: maxRetries,sleepDurationProvider: times =>TimeSpan.FromMilliseconds(times*300));
        }
        public void Start()
        {
            var result = UpdateUserRegisteredRSS();
            if (result.Result)
                logger.LogDebug("User Feeds Updated Succesfully");
            logger.LogDebug("User Feeds Failed To Updated");
            Thread.Sleep(1000);

        }
        public async Task<bool> UpdateUserRegisteredRSS()
        {
            try
            {
                var userSubscriptions = unitOfWork.UserSubscription.GetAll();
                foreach (var subscription in userSubscriptions)
                {
                    var feed = await RetryPolicy(subscription.RSSURL);
                    var a = counter;
                    if (feed == null)
                        continue;
                    var listOfRSS = new List<RSS>();
                    foreach (var item in feed.Items)
                    {
                        try
                        {
                            var rss = CreateRSS(subscription, item);
                            var isExisted = unitOfWork.RSS.CkeckIfExist(rss.UserID, rss.Link);
                            if (!isExisted)
                                unitOfWork.RSS.Add(rss);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }

                }
                unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                //logger.LogError(ex.Message);
                return false;
            }

        }
        private RSS CreateRSS(UserSubscription subscription, SyndicationItem item)
        {
            return new RSS()
            {
                UserID = subscription.UserId,
                Link = item.Links.ToList()[0].Uri.ToString(),
                Title = item.Title.Text,//title
                LinkId = item.Id,//guid
                Desc = item.Summary.Text,
                PublishDate = item.PublishDate.ToString(),
                BaseUrl = subscription.RSSURL
            };
        }
        private async Task<SyndicationFeed> GetFeed(String url)
        {
            var task = Task.Factory.StartNew(() =>
            {
                try
                {
                    XmlReader reader = XmlReader.Create(url);
                    return SyndicationFeed.Load(reader);
                }
                catch(Exception ex)
                {
                    return null;
                }
            });
            int timeout = 5000;
            if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
            {
                return await task;
            }
            else
            {
                throw new HttpRequestException();
            }
        }
        private async Task<SyndicationFeed> RetryPolicy(string url)
        {
            try
            {
                return await retryPolicy.ExecuteAsync(async () =>
                {
                    counter++;
                    return await GetFeed(url);
                });
            }
            catch (Exception ex)
            { 
                return null; 
            }
        }
    }
}
