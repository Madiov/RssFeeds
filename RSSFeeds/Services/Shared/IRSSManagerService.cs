using RSSFeeds.Services.DTO;

namespace RSSFeeds.Services.Shared
{
    public interface IRSSManagerService
    {
        Task<BaseResponseDTO> RegisterRSS(string userId, RegisterDTO registerDTO);
        BaseResponseDTO GetUserFeeds(string userId);
        Task<BaseResponseDTO> BookMarkRss(string userId, LinkDTO linkDTO);
        Task<BaseResponseDTO> SetReaded(string userId, LinkDTO linkDTO);
        Task<BaseResponseDTO> SubmitComment(string userId ,CommentDTO commentDTO);


    }
}
