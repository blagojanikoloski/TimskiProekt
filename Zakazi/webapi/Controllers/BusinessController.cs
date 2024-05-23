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
        private readonly IPostService _postService;

        public BusinessController(IBusinessService businessService, IPostService postService)
        {
            _businessService = businessService;
            _postService = postService;
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

        [HttpGet("owner/{ownerId}/businesses")]
        public async Task<ActionResult<IEnumerable<Business>>> GetBusinessesByOwnerId(int ownerId)
        {
            try
            {
                var businesses = await _businessService.GetBusinessesByOwnerIdAsync(ownerId);
                return Ok(businesses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("businesses")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinesses()
        {
            try
            {
                var businesses =  _businessService.GetAllBusinesses();
                return Ok(businesses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("{businessId}")]
        public async Task<ActionResult> DeleteBusiness(int businessId)
        {
            try
            {
                // Delete all posts associated with the business
                await _postService.DeletePostsByBusinessId(businessId);

                // Then delete the business
                var deletedBusiness = await _businessService.DeleteBusinessAsync(businessId);
                if (deletedBusiness == null)
                    return NotFound($"Business with id {businessId} not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
