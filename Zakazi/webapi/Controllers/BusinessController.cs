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
    [Authorize]
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
        public async Task<ActionResult<Business>> CreateBusiness(BusinessCreationDto businessCreationDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return Unauthorized();
            }

            var business = new Business();
            _mapper.Map(businessCreationDto, business);
            business.OwnerId = userId;
            return Ok(await _businessService.CreateBusinessAsync(business));
        }

        [HttpGet("owner/{ownerId}/businesses")]
        public async Task<ActionResult<IEnumerable<Business>>> GetBusinessesByOwnerId(int ownerId)
        {
            return Ok(await _businessService.GetBusinessesByOwnerIdAsync(ownerId));
        }

        [HttpGet("current-user/businesses")]
        public async Task<ActionResult<IEnumerable<Business>>> GetCurrentUserBusinesses()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(await _businessService.GetBusinessesByOwnerIdAsync(userId));
        }

        [AllowAnonymous]
        [HttpGet("businesses")]
        public async Task<ActionResult<IEnumerable<Business>>> GetAllBusinesses()
        {
           return Ok(await _businessService.GetAllBusinesses());
        }


        // will only work if business does not have any requests sent to it
        [HttpDelete("{businessId}")]
        public async Task<ActionResult<Business>> DeleteBusiness(int businessId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return Unauthorized();
            }

            var business = await _businessService.GetBusinessById(businessId);

            if(business != null && business.OwnerId == userId)
            {
                return await _businessService.DeleteBusinessAsync(business);
            }
            return Unauthorized();
        }


    }
}
