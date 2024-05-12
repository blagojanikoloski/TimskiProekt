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

        public RequestsController(IRequestService requestService, ILogger<RequestsController> logger, IZakaziUserService userService, IBusinessService businessService, IPostService postService)
        {
            _requestService = requestService;
            _logger = logger;
            _userService = userService;
            _businessService = businessService;
            _postService = postService;
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

        //[HttpGet("GetMyRequests/{id}")]
        //public async Task<ActionResult<IEnumerable<Request>>> GetMyRequests(string id)
        //{
        //    try
        //    {
        //        var requests = await _requestService.GetRequestsByClientIdOrWorkerId(id);
        //        return Ok(requests);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it appropriately
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving requests from the database.");
        //    }
        //}

         // Import the namespace where RequestStatus enum is defined

        [HttpGet("client/{clientId}/requests")]
            public async Task<ActionResult<IEnumerable<MyProfileCardDto>>> GetRequestsByClientId(int clientId)
            {
                try
                {
                    var requests = await _requestService.GetRequestsByClientId(clientId);
                    var dtos = new List<MyProfileCardDto>();

                    foreach (var request in requests)
                    {
                        // Fetch user information using the client ID
                        var user = await _userService.GetUserById(request.ClientId.ToString());
                        var business = await _businessService.GetBusinessById(request.BusinessId);
                        var post = await _postService.GetPostById(request.PostId);

                        // Convert RequestStatus enum to string
                        string requestStatusString = Enum.GetName(typeof(RequestStatus), request.RequestStatus);

                        // Create a DTO and append user information
                        var dto = new MyProfileCardDto
                        {
                            RequestId = request.RequestId,
                            Timestamp = request.Timestamp,
                            RequestStatus = request.RequestStatus,
                            PostId = request.PostId,
                            BusinessId = request.BusinessId,
                            ClientId = request.ClientId,
                            From = request.From.ToString("yyyy-MM-dd HH:mm:ss"),
                            To = request.To.ToString("yyyy-MM-dd HH:mm:ss"),
                            Name = user.Name,
                            Surname = user.Surname,
                            BusinessName = business.BusinessName,
                            NameOfService = post.NameOfService,
                            Price = post.Price,
                            RequestStatusInString = requestStatusString // Assign the string representation of RequestStatus
                        };
                        dtos.Add(dto);
                    }
                    return Ok(dtos);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

















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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            try
            {
                await _requestService.DeleteRequest(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the request.");
            }
        }

        [HttpPost("request")]
        public async Task<ActionResult<Request>> CreateRequest([FromBody] Request request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Request data is null.");
                }

                // You may want to perform additional validation here if necessary

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

        


    }
}
