using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using webapi.Domain.DTOs;
using webapi.Domain.Models;
using webapi.Domain.Services;

namespace webapi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly UserManager<ZakaziUser> _userManager;

        public BusinessController(IBusinessService businessService, IPostService postService, UserManager<ZakaziUser> userManager, IMapper mapper)
        {
            _businessService = businessService;
            _postService = postService;
            _userManager = userManager;
            _mapper = mapper;
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

        //[HttpPost("business")]
        //public async Task<ActionResult<Business>> CreateBusiness(BusinessCreationDto businessCreationDto)
        //{
        //    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    var user = await _userManager.FindByIdAsync(userId.ToString());

        //    if (user == null)
        //    {
        //        return Unauthorized();
        //    }

        //    var business = new Business();
        //    _mapper.Map(businessCreationDto, business);
        //    business.OwnerId = userId;
        //    return Ok(await _businessService.CreateBusinessAsync(business));
        //}

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
