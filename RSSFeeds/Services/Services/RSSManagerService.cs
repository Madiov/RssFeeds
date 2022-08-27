using Fanap.MarginParkingPlus.Services.Shared;
using Newtonsoft.Json;
using RSSFeeds.Database.Models;
using RSSFeeds.Database.Shared;
using RSSFeeds.Services.DTO;
using RSSFeeds.Services.Shared;
using System.Security.Cryptography;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace RSSFeeds.Services.Services
{
    public class RSSManagerService : BaseService, IRSSManagerService
    {
        public RSSManagerService(ILoggerManager logger, IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork, logger, configuration)
        {

        }
        public BaseResponseDTO GetUserFeeds(string userId)
        {
            try
            {
                var RSSFeedDTO = new List<RSSDTO>();
                var usersRSSFeed = unitOfWork.RSS.GetUserFeed(userId);
                foreach (var RSS in usersRSSFeed)
                {
                    var comments = unitOfWork.RSSComment.GetRssComments(RSS.Link);
                    RSSFeedDTO.Add(new RSSDTO
                    {
                        Title = RSS.Title,
                        Desc = RSS.Desc,
                        Link = RSS.Link,
                        LinkId = RSS.LinkId,//guid
                        Date = RSS.PublishDate,
                        isReaded = RSS.isRead,
                        isBookmarked = RSS.isBookmarked,
                        Comments = comments.Select(c => c.Comment).ToList()
                    });
                }
                return new BaseResponseDTO()
                {
                    Message = JsonConvert.SerializeObject(RSSFeedDTO),
                    Result = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseDTO
                {
                    Message = ex.Message.ToString(),
                    Result = false
                };
            }
        }
        public async Task<BaseResponseDTO> RegisterRSS(string userId, RegisterDTO registerDTO)
        {
            Uri uriResult;
            try
            {
                bool result = Uri.TryCreate(registerDTO.Url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if (registerDTO.Url == "" || !result)
                    return new LoginResponseDTO { Result = false, Message = "Invalid Url" };
                var subscribtion = unitOfWork.UserSubscription.Get(us => us.UserId == userId && us.RSSURL == registerDTO.Url);
                if (subscribtion != null)
                {
                    return new LoginResponseDTO { Result = false, Message = "You Have Already Subscribed To This Url" };
                }

                var userSubscription = new UserSubscription()
                {
                    UserId = userId,
                    RSSURL = registerDTO.Url,
                };
                unitOfWork.UserSubscription.Add(userSubscription);
                await unitOfWork.SaveAsync();
                return new BaseResponseDTO { Result = true, Message = registerDTO.Url + " Has Been Subscribed" };
            }
            catch (Exception ex)
            {
                return new BaseResponseDTO { Message = ex.Message.ToString(), Result = false };
            }
        }

        public async Task<BaseResponseDTO> BookMarkRss(string userId, LinkDTO linkDTO)
        {
            try
            {
                var RSS = unitOfWork.RSS.Get(r => r.UserID == userId && r.Link == linkDTO.Link);
                if (RSS == null)
                    return new LoginResponseDTO { Result = false, Message = "There Is No RSS With This Link" };
                RSS.isBookmarked = true;
                unitOfWork.RSS.Update(RSS);
                await unitOfWork.SaveAsync();
                return new BaseResponseDTO { Message = "RSS Bookmarked Succesfully", Result = true };
            }

            catch (Exception ex)
            {
                return new BaseResponseDTO { Message = ex.Message.ToString(), Result = false };
            }

        }
        public async Task<BaseResponseDTO> SetReaded(string userId, LinkDTO linkDTO)
        {
            try
            {
                var RSS = unitOfWork.RSS.Get(r => r.UserID == userId && r.Link == linkDTO.Link);
                if (RSS == null)
                    return new LoginResponseDTO { Result = false, Message = "There Is No RSS With This Link" };
                RSS.isRead = true;
                unitOfWork.RSS.Update(RSS);
                await unitOfWork.SaveAsync();
                return new BaseResponseDTO { Message = "RSS Bookmarked Succesfully", Result = true };
            }
            catch (Exception ex)
            {
                return new BaseResponseDTO { Message = ex.Message.ToString(), Result = false };
            }

        }
        public async Task<BaseResponseDTO> SubmitComment(string userId, CommentDTO commentDTO)
        {
            try
            {
                var RSS = unitOfWork.RSS.Get(r => r.UserID == userId && r.Link == commentDTO.Link);
                if (RSS == null)
                    return new LoginResponseDTO { Result = false, Message = "There Is No RSS With This Link" };
                var RSSComment = new RSSComment()
                {
                    UserId = userId,
                    Comment = commentDTO.Comment,
                    Link = commentDTO.Link,
                };
                unitOfWork.RSSComment.Add(RSSComment);
                await unitOfWork.SaveAsync();
                return new BaseResponseDTO { Message = "Comment Submited Succesfully", Result = true };
            }
            catch (Exception ex)
            {
                return new BaseResponseDTO { Message = ex.Message.ToString(), Result = false };
            }

        }

    }
}
