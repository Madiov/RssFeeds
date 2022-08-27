using Fanap.MarginParkingPlus.Services.Shared;
using Microsoft.Extensions.Configuration;
using Moq;
using RSSFeeds.Database.Models;
using RSSFeeds.Database.Shared;
using RSSFeeds.Services.DTO;
using RSSFeeds.Services.Services;
using System.Linq.Expressions;

namespace RSSFeeds.Test
{
    public class RSSManagerTest
    {
        Mock<IUnitOfWork> _IUnitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
        Mock<ILoggerManager> _ILoggerManager = new Mock<ILoggerManager>(MockBehavior.Strict);
        IConfiguration _IConfiguration;
        RSSManagerService RSSmanager;
        public RSSManagerTest()
        {
            _IConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"JwtSetting:SecretKey", "KeyToHashWith16Character" },
                })
                .Build();
            RSSmanager = new RSSManagerService(_ILoggerManager.Object, _IUnitOfWork.Object, _IConfiguration);
        }
        [Test]
        public async Task RegisterRSS_ValidInput_ReturnsTrue()
        {
            var registerDTO = new RegisterDTO();
            registerDTO.Url = "https://news.un.org/feed/subscribe/en/news/region/middle-east/feed/rss.xml";
            _IUnitOfWork.Setup(p => p.UserSubscription.Get(It.IsAny<Expression<Func<UserSubscription, bool>>>())).Returns<IUnitOfWork>(null);
            _IUnitOfWork.Setup(p => p.UserSubscription.Add(It.IsAny<UserSubscription>())).Verifiable();
            _IUnitOfWork.Setup(p => p.SaveAsync()).ReturnsAsync(1);
            var result = await RSSmanager.RegisterRSS(It.IsAny<string>(), registerDTO);
            Assert.True(result.Result);
        }
        [Test]
        public async Task RegisterRSS_InvalidUrl_ReturnsFalse()
        {
            var registerDTO = new RegisterDTO();
            registerDTO.Url = "htts://news.un.org/feed/subscribe/en/news/region/middle-east/feed/rss.xml";
            var result = await RSSmanager.RegisterRSS(It.IsAny<string>(), registerDTO);
            Assert.False(result.Result);
        }
        [Test]
        public async Task RegisterRSS_AlreadySubscribed_ReturnsFalse()
        {
            var registerDTO = new RegisterDTO();
            registerDTO.Url = "https://news.un.org/feed/subscribe/en/news/region/middle-east/feed/rss.xml";
            _IUnitOfWork.Setup(p => p.UserSubscription.Get(It.IsAny<Expression<Func<UserSubscription, bool>>>())).Returns(new UserSubscription { UserId ="1",RSSURL= "https://news.un.org/feed/subscribe/en/news/region/middle-east/feed/rss.xml" });
            var result = await RSSmanager.RegisterRSS(It.IsAny<string>(), registerDTO);
            Assert.False(result.Result);
            
        }
        [Test]
        public async Task BookMarkRss_ValidUrl_ReturnsTrue()
        {
            var linkDTO = new LinkDTO();
            var RSSMock = new Mock<RSS>();
            linkDTO.Link = "https://news.un.org/feed/subscribe/en/news/region/middle-east/feed/rss.xml";
            _IUnitOfWork.Setup(p => p.RSS.Get(It.IsAny<Expression<Func<RSS, bool>>>())).Returns(RSSMock.Object);
            _IUnitOfWork.Setup(p => p.RSS.Update(It.IsAny<RSS>())).Verifiable();
            _IUnitOfWork.Setup(p => p.SaveAsync()).ReturnsAsync(1);
            var result = await RSSmanager.BookMarkRss(It.IsAny<string>(), linkDTO);
            Assert.True(result.Result);
        }
        [Test]
        public async Task BookMarkRss_InvalidUrl_ReturnsFalse()
        {
            var userId = "1";
            var linkDTO = new LinkDTO();
            linkDTO.Link = "https://news.un.org/feed/subscribe/en/news/region/middle-east/feed/rss.xml";
            _IUnitOfWork.Setup(p => p.RSS.Get(It.IsAny<Expression<Func<RSS, bool>>>())).Returns<IUnitOfWork>(null);
            var result = await RSSmanager.BookMarkRss(It.IsAny<string>(), linkDTO);
            Assert.False(result.Result);
        }
        [Test]
        public async Task SetReaded_ValidUrl_ReturnsTrue()
        {
            var linkDTO = new LinkDTO();
            var RSSMock = new Mock<RSS>();
            linkDTO.Link = "https://news.un.org/feed/subscribe/en/news/region/middle-east/feed/rss.xml";
            _IUnitOfWork.Setup(p => p.RSS.Get(It.IsAny<Expression<Func<RSS, bool>>>())).Returns(RSSMock.Object);
            _IUnitOfWork.Setup(p => p.RSS.Update(It.IsAny<RSS>())).Verifiable();
            _IUnitOfWork.Setup(p => p.SaveAsync()).ReturnsAsync(1);
            var result = await RSSmanager.SetReaded(It.IsAny<string>(), linkDTO);
            Assert.True(result.Result);
        }
        [Test]
        public async Task SetReaded_InvalidUrl_ReturnsFalse()
        {
            var linkDTO = new LinkDTO();
            linkDTO.Link = "https://news.un.org/feed/subscribe/en/news/region/middle-east/feed/rss.xml";
            _IUnitOfWork.Setup(p => p.RSS.Get(It.IsAny<Expression<Func<RSS, bool>>>())).Returns<IUnitOfWork>(null);
            var result = await RSSmanager.SetReaded(It.IsAny<string>(), linkDTO);
            Assert.False(result.Result);
        }
        [Test]
        public async Task SubmitComment_validUrl_ReturnsFalse()
        {
            var linkDTO = new LinkDTO();
            var RSSMock = new Mock<RSS>();
            linkDTO.Link = "https://news.un.org/feed/subscribe/en/news/region/middle-east/feed/rss.xml";
            _IUnitOfWork.Setup(p => p.RSS.Get(It.IsAny<Expression<Func<RSS, bool>>>())).Returns(RSSMock.Object);
            _IUnitOfWork.Setup(p => p.RSSComment.Add(It.IsAny<RSSComment>())).Verifiable();
            _IUnitOfWork.Setup(p => p.SaveAsync()).ReturnsAsync(1);
            var result = await RSSmanager.SetReaded(It.IsAny<string>(), linkDTO);
            Assert.True(result.Result);
        }
        [Test]
        public async Task SubmitComment_InvalidUrl_ReturnsFalse()
        {
            var linkDTO = new LinkDTO();
            linkDTO.Link = "https://news.un.org/feed/subscribe/en/news/region/middle-east/feed/rss.xml";
            _IUnitOfWork.Setup(p => p.RSS.Get(It.IsAny<Expression<Func<RSS, bool>>>())).Returns<IUnitOfWork>(null);
            var result = await RSSmanager.SetReaded(It.IsAny<string>(), linkDTO);
            Assert.False(result.Result);
        }
        [Test]
        public void GetUserFeeds_ReturnsTrue()
        {
            var RSSListMock = new Mock<List<RSS>>();
            var RSSCommentListMock = new Mock<List<RSSComment>>();
            _IUnitOfWork.Setup(p => p.RSS.GetUserFeed(It.IsAny<string>())).Returns(RSSListMock.Object);
            _IUnitOfWork.Setup(p => p.RSSComment.GetRssComments(It.IsAny<string>())).Returns(RSSCommentListMock.Object);
            var result =  RSSmanager.GetUserFeeds(It.IsAny<string>());
            Assert.True(result.Result);
        }


    }
}
