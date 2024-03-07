using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Domain.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        [HttpGet]
        [ActionName("GetAllRequests")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllRequests()
        {
            //return Ok(await _requestService.GetAllRequests());
            return Ok();
        }
    }
}
