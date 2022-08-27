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
    public class UserManagerTest
    {
        Mock<IUnitOfWork> _IUnitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
        Mock<ILoggerManager> _ILoggerManager = new Mock<ILoggerManager>(MockBehavior.Strict);
        IConfiguration _IConfiguration;
        UserManagerService Usermanager;

        public UserManagerTest( )
        {
            _IConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"JwtSetting:SecretKey", "KeyToHashWith16Character" },
                    { "JwtSetting:ExpirationTimeHRS" ,"1" }
                })
                .Build();
            Usermanager = new UserManagerService(_ILoggerManager.Object, _IUnitOfWork.Object, _IConfiguration);
        }

        [Test]
        public void LoginUser_ValidInput_ReturnsTrue()
        {
            var user = new UserDTO();
            user.Username = "abcd";
            user.Password = "string3";
            _IUnitOfWork.Setup(p => p.User.Get((It.IsAny<Expression<Func<User, bool>>>()))).Returns(new User() { Password = "9E6DC8685BF3C1B338F2011ACE904887", UserName = "abcd" });
            var result = Usermanager.LoginUser(user);
            Assert.True(result.Result);
            Assert.IsNotEmpty(result.Jwtoken);
        }
        [Test]
        public void LoginUser_EmptyInput_ReturnsFalse()
        {
            var user = new UserDTO();
            user.Username = "";
            user.Password = "123";
            _IUnitOfWork.Setup(p => p.User.Get((It.IsAny<Expression<Func<User, bool>>>()))).Returns<IUnitOfWork>(null);
            var result = Usermanager.LoginUser(user);
            Assert.False(result.Result);
            
        }
        [Test]
        public void LoginUser_UserDoesNotExists_ReturnsFalse()
        {
            var user = new UserDTO();
            _IUnitOfWork.Setup(p => p.User.Get(It.IsAny<Expression<Func<User, bool>>>())).Returns<IUnitOfWork>(null);
            user.Username = "abcd";
            user.Password = "123";
            var result = Usermanager.LoginUser(user);
            Assert.False(result.Result);
        }
        [Test]
        public async Task RegisterUser_ValidInput_ReturnsTrue()
        {
            var user = new UserDTO();
            user.Username = "abcd";
            user.Password = "123";
            _IUnitOfWork.Setup(p => p.User.Get((It.IsAny<Expression<Func<User, bool>>>()))).Returns<IUnitOfWork>(null);
            _IUnitOfWork.Setup(p => p.User.Add(It.IsAny<User>())).Verifiable();
            _IUnitOfWork.Setup(p => p.SaveAsync()).ReturnsAsync(1);
            var result = await Usermanager.RegisterUser(user);
            Assert.True(result.Result);


        }
        [Test]
        public async Task RegisterUser_EmptyInput_ReturnsFalse()
        {
            var user = new UserDTO();
            user.Username = "";
            user.Password = "123";
            var result = await Usermanager.RegisterUser(user);
            Assert.False(result.Result);
        }
        [Test]
        public async Task RegisterUser_UserAlreadyExists_ReturnsFalse()
        {
            var user = new UserDTO();
            user.Username = "abcd";
            user.Password = "123";
            _IUnitOfWork.Setup(p => p.User.Get(u => u.UserName == user.Username)).Returns(new User() { Password = "123", UserName = "abcd" });
            var result = await Usermanager.RegisterUser(user);
            Assert.False(result.Result);
        }
    }
}