using RSSFeeds.Database.Models;
using RSSFeeds.Services.DTO;

namespace RSSFeeds.Services.Shared
{
    public interface IUserManagerService
    {
        LoginResponseDTO LoginUser(UserDTO request);
        Task<BaseResponseDTO> RegisterUser(UserDTO request);

    }
}
