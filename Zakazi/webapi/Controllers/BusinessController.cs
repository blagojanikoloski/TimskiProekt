using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using webapi.Domain.Models;
using webapi.Domain.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpPost("business")]
        public async Task<ActionResult<Business>> CreateBusiness([FromBody] Business model)
        {
            try
            {
                var business = await _businessService.CreateBusinessAsync(model.OwnerId, model.BusinessName);
                return Ok(business);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
