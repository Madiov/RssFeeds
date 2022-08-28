using Fanap.MarginParkingPlus.Services.Shared;
using Microsoft.Extensions.Configuration;
using Moq;
using RSSFeeds.Database.Models;
using RSSFeeds.Database.Shared;
using RSSFeeds.Services.Services;


namespace RSSFeeds.Test
{
    public class RSSUpdaterTest
    {
        Mock<IUnitOfWork> _IUnitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
        Mock<ILoggerManager> _ILoggerManager = new Mock<ILoggerManager>(MockBehavior.Strict);
        IConfiguration _IConfiguration;
        RSSUpdaterService RSSUpdaterService;
        public RSSUpdaterTest()
        {
            _IConfiguration = new ConfigurationBuilder()
               .AddInMemoryCollection(new Dictionary<string, string>
               {
                    {"JwtSetting:SecretKey", "KeyToHashWith16Character" },
                    { "JwtSetting:ExpirationTimeHRS" ,"1" }
               })
               .Build();
            RSSUpdaterService = new RSSUpdaterService(_ILoggerManager.Object, _IUnitOfWork.Object, _IConfiguration);
        }
        [Test]
        public async Task UpdateUserRegisteredRSS_WithNewRSSFeed_ReturnTrue()
        {
            var userSubscriptionMock = new List<UserSubscription>()
            {
                new UserSubscription()
                {
                    UserId=It.IsAny<string>(),
                    RSSURL="https://www.farsnews.ir/rss"
                }
            };
            _IUnitOfWork.Setup(us => us.UserSubscription.GetAll()).Returns(userSubscriptionMock);
            _IUnitOfWork.Setup(r=>r.RSS.CkeckIfExist(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            _IUnitOfWork.Setup(r => r.RSS.Add(It.IsAny<RSS>())).Verifiable();
            _IUnitOfWork.Setup(u => u.Save()).Returns(1);
            var result = await RSSUpdaterService.UpdateUserRegisteredRSS();
            Assert.True(result);
        }
        [Test]
        public async Task UpdateUserRegisteredRSS_WithoutNewRSSFeed_ReturnTrue()
        {
            var userSubscriptionMock = new List<UserSubscription>()
            {
                new UserSubscription()
                {
                    UserId=It.IsAny<string>(),
                    RSSURL="https://www.farsnews.ir/rss"
                }
            };
            _IUnitOfWork.Setup(us => us.UserSubscription.GetAll()).Returns(userSubscriptionMock);

            _IUnitOfWork.Setup(r => r.RSS.CkeckIfExist(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _IUnitOfWork.Setup(u => u.Save()).Returns(1);
            var result = await RSSUpdaterService.UpdateUserRegisteredRSS();
            Assert.True(result);
        }
        [Test]
        public async Task UpdateUserRegisteredRSS_BadUrl_ReturnTrue()
        {
            var userSubscriptionMock = new List<UserSubscription>()
            {
                new UserSubscription()
                {
                    UserId=It.IsAny<string>(),
                    RSSURL="https://www.farsnews.ir/rs"
                }
            };
            _IUnitOfWork.Setup(us => us.UserSubscription.GetAll()).Returns(userSubscriptionMock);
            _IUnitOfWork.Setup(r => r.RSS.CkeckIfExist(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _IUnitOfWork.Setup(u => u.Save()).Returns(1);
            var result = await RSSUpdaterService.UpdateUserRegisteredRSS();
            Assert.True(result);
        }
    }
}
