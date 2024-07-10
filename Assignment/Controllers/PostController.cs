using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class PostController : ControllerBase
    {
        [HttpGet("posts")]
        public async Task<IActionResult> GetPosts()
        {

            var posts = new List<dynamic>();
            var url = "https://jsonplaceholder.typicode.com/posts/";
            posts = await Infrastructure.Common.Utility.GetData(url);
            return Ok(posts);

        }
    }
}
