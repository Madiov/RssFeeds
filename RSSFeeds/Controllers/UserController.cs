using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RSSFeeds.Database.Models;
using RSSFeeds.Services.DTO;
using RSSFeeds.Services.Shared;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RSSFeeds.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserManagerService userManagerService;
        public UserController(IUserManagerService userManagerService)
        {
            this.userManagerService = userManagerService;
        }
        [HttpPost(Name ="Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO request)
        {
            var result = await userManagerService.RegisterUser(request);
            return Ok(result);
        }
        [HttpPost("Login")]
        public IActionResult LoginUser([FromBody] UserDTO request)
        {
            var rsult = userManagerService.LoginUser(request);
            return Ok(rsult);
        }
    }
}
