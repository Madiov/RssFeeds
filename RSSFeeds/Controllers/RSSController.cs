using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSSFeeds.Services.DTO;
using RSSFeeds.Services.Shared;
using System.Security.Claims;

namespace RSSFeeds.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class RSSController : ControllerBase
    {
        private readonly IRSSManagerService RSSManagerService;
        public RSSController(IRSSManagerService RSSManagerService)
        {
            this.RSSManagerService = RSSManagerService;
        }
        [HttpGet("Feeds")]
        public IActionResult GetUserFeeds()
        {
            string? userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (userId == null)
                return NotFound();
            var result = RSSManagerService.GetUserFeeds(userId);
            if (result.Result)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterRSS([FromBody] RegisterDTO registerDTO)
        {
            string? userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (userId == null)
                return NotFound();
            var result = await RSSManagerService.RegisterRSS(userId, registerDTO);
            if (result.Result)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }
        [HttpPut("Bookmark")]
        public async Task<IActionResult> BookmarkRSS([FromBody] LinkDTO linkDTO)
        {
            string? userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (userId == null)
                return BadRequest();
            var result = await RSSManagerService.BookMarkRss(userId, linkDTO);
            if (result.Result)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }
        [HttpPut("SetRead")]
        public async Task<IActionResult> SetRead([FromBody] LinkDTO linkDTO)
        {
            string? userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (userId == null)
                return NotFound();
            var result =await RSSManagerService.SetReaded(userId, linkDTO);
            return Ok(result);
        }
        [HttpPost("Comment")]
        public async Task<IActionResult> SubmitComment([FromBody] CommentDTO commentDTO)
        {
            string? userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (userId == null)
                return NotFound();
            var result = await RSSManagerService.SubmitComment(userId, commentDTO);
            return  Ok(result);
        }
    }
}