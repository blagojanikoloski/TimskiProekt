using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.Domain.Models;
using webapi.Domain.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using webapi.Domain.DTOs;
using System.ComponentModel.DataAnnotations;
using webapi.Domain.Enumerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace webapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IZakaziUserService _userService;
        private readonly IRequestService _requestService;
        private readonly ILogger<RequestsController> _logger;
        private readonly IBusinessService _businessService;
        private readonly IPostService _postService;
        private readonly UserManager<ZakaziUser> _userManager;


        public RequestsController(IRequestService requestService, ILogger<RequestsController> logger, IZakaziUserService userService, IBusinessService businessService, IPostService postService, UserManager<ZakaziUser> userManager)
        {
            _requestService = requestService;
            _logger = logger;
            _userService = userService;
            _businessService = businessService;
            _postService = postService;
            _userManager = userManager;
        }

        [HttpGet]
        [ActionName("GetAllRequests")]
        public async Task<ActionResult<IEnumerable<Request>>> GetAllRequests()
        {
            try
            {
                var requests = await _requestService.GetAllRequests();
                return Ok(requests);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving requests from the database.");
            }
        }


        // Import the namespace where RequestStatus enum is defined

        [HttpGet("client/{clientId}/requests")]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetRequestsByClientId(int clientId)
        {

            var client = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == clientId);
            if (client == null)
            {
                return Unauthorized();
            }
            return Ok(await _requestService.GetRequestsByClient(client));
        }

        [Authorize]
        [HttpGet("currentUser/requests")]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetCurrentUserRequests()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var client = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (client == null)
            {
                return Unauthorized();
            }
            return Ok(await _requestService.GetRequestsByClient(client));
        }

        //[HttpGet("client/{clientId}/requests")]
        //public async Task<ActionResult<IEnumerable<MyProfileCardDto>>> GetRequestsByClientId(int clientId)
        //{
        //    try
        //    {
        //        var requests = await _requestService.GetRequestsByClientId(clientId);
        //        var dtos = new List<MyProfileCardDto>();

        //        foreach (var request in requests)
        //        {
        //            // Fetch user information using the client ID
        //            var user = await _userService.GetUserById(request.ClientId.ToString());
        //            var business = await _businessService.GetBusinessById(request.BusinessId);
        //            var post = await _postService.GetPostById(request.PostId);
        //            var owner = await _userService.GetUserById(business.OwnerId.ToString());

        //            // Convert RequestStatus enum to string
        //            string requestStatusString = Enum.GetName(typeof(RequestStatus), request.RequestStatus);

        //            // Create a DTO and append user information
        //            var dto = new MyProfileCardDto
        //            {
        //                RequestId = request.RequestId,
        //                Timestamp = request.Timestamp,
        //                RequestStatus = request.RequestStatus,
        //                PostId = request.PostId,
        //                BusinessId = request.BusinessId,
        //                ClientId = request.ClientId,
        //                From = request.From.ToString("yyyy-MM-dd HH:mm:ss"),
        //                To = request.To.ToString("yyyy-MM-dd HH:mm:ss"),
        //                Name = owner.Name,
        //                Surname = owner.Surname,
        //                BusinessName = business.BusinessName,
        //                NameOfService = post.NameOfService,
        //                Price = post.Price,
        //                RequestStatusInString = requestStatusString // Assign the string representation of RequestStatus
        //            };
        //            dtos.Add(dto);
        //        }
        //        return Ok(dtos);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        //[HttpGet("worker/{workerId}/requests")]
        //public async Task<ActionResult<IEnumerable<MyProfileCardDto>>> GetRequestsByWorkerId(int workerId)
        //{
        //    try
        //    {
        //        var requests = await _requestService.GetRequestsByWorkerId(workerId);
        //        var dtos = new List<MyProfileCardDto>();

        //        foreach (var request in requests)
        //        {
        //            // Fetch user information using the client ID
        //            var user = await _userService.GetUserById(request.ClientId.ToString());
        //            var business = await _businessService.GetBusinessById(request.BusinessId);
        //            var post = await _postService.GetPostById(request.PostId);
        //            var owner = await _userService.GetUserById(business.OwnerId.ToString());

        //            // Convert RequestStatus enum to string
        //            string requestStatusString = Enum.GetName(typeof(RequestStatus), request.RequestStatus);

        //            // Create a DTO and append user information
        //            var dto = new MyProfileCardDto
        //            {
        //                RequestId = request.RequestId,
        //                Timestamp = request.Timestamp,
        //                RequestStatus = request.RequestStatus,
        //                PostId = request.PostId,
        //                BusinessId = request.BusinessId,
        //                ClientId = request.ClientId,
        //                From = request.From.ToString("yyyy-MM-dd HH:mm:ss"),
        //                To = request.To.ToString("yyyy-MM-dd HH:mm:ss"),
        //                Name = user.Name,
        //                Surname = user.Surname,
        //                BusinessName = business.BusinessName,
        //                NameOfService = post.NameOfService,
        //                Price = post.Price,
        //                RequestStatusInString = requestStatusString // Assign the string representation of RequestStatus
        //            };
        //            dtos.Add(dto);
        //        }
        //        return Ok(dtos);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}


        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequestById(int id)
        {
            try
            {
                var request = await _requestService.GetRequestById(id);
                if (request == null)
                {
                    return NotFound();
                }
                return Ok(request);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving the request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(int id, Request request)
        {
            if (id != request.RequestId)
            {
                return BadRequest();
            }

            try
            {
                await _requestService.UpdateRequest(request);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the request.");
            }
        }


        [HttpPost("request")]
        public async Task<ActionResult<Request>> CreateRequest([FromBody] RequestCreationDto requestDto)
        {
            try
            {
                if (requestDto == null)
                {
                    return BadRequest("Request data is null.");
                }

                // Fetch posts individually by IDs
                var posts = new List<Post>();
                foreach (var postId in requestDto.PostIds)
                {
                    var post = await _postService.GetPostById(postId);
                    if (post != null)
                    {
                        posts.Add(post);
                    }
                    else
                    {
                        // Handle the case where a post with the given ID is not found
                        return NotFound($"Post with ID {postId} not found.");
                    }
                }

                // Create the request object
                var request = new Request
                {
                    // Assign other properties of the request
                    Timestamp = DateTimeOffset.Now,
                    RequestStatus = RequestStatus.PENDING,
                    BusinessId = requestDto.BusinessId,
                    ClientId = requestDto.ClientId,
                    From = requestDto.From,
                    To = requestDto.To,
                    Posts = posts // Assign fetched posts to the request
                };

                // Call the service method to create the request
                var createdRequest = await _requestService.CreateRequest(request);

                // Return the created request with a 201 Created status
                return CreatedAtAction(nameof(GetRequestById), new { id = createdRequest.RequestId }, createdRequest);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating the request.");
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            try
            {
                var request = await _requestService.GetRequestById(id);
                if (request == null)
                {
                    return NotFound();
                }

                await _requestService.DeleteRequest(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the request.");
            }
        }




        [HttpPut("accept/{id}")]
        public async Task<IActionResult> AcceptRequest(int id)
        {
            try
            {
                var request = await _requestService.GetRequestById(id);
                if (request == null)
                {
                    return NotFound();
                }

                request.RequestStatus = RequestStatus.ACCEPTED;
                await _requestService.UpdateRequest(request);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error accepting the request.");
            }
        }

        [HttpPut("decline/{id}")]
        public async Task<IActionResult> DeclineRequest(int id)
        {
            try
            {
                var request = await _requestService.GetRequestById(id);
                if (request == null)
                {
                    return NotFound();
                }

                request.RequestStatus = RequestStatus.REJECTED;
                await _requestService.UpdateRequest(request);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error declining the request.");
            }
        }


    }
}
