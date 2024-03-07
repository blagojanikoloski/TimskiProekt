using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Domain.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        [HttpGet]
        [ActionName("GetAllPosts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {
            //return Ok(await _postService.GetAllPosts());
            return Ok();
        }
    }
}
