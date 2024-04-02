using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.Domain.Models;
using webapi.Domain.Services;
using Microsoft.Extensions.Logging;


namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly ILogger<RequestsController> _logger;

        public RequestsController(IRequestService requestService, ILogger<RequestsController> logger)
        {
            _requestService = requestService;
            _logger = logger;
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

        [HttpGet("GetMyRequests/{id}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetMyRequests(string id)
        {
            try
            {
                var requests = await _requestService.GetRequestsByClientIdOrWorkerId(id);
                return Ok(requests);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving requests from the database.");
            }
        }













        [HttpPost]
        public async Task<ActionResult<Request>> CreateRequest(Request request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdRequest = await _requestService.CreateRequest(request);
                return CreatedAtAction(nameof(GetRequestById), new { id = createdRequest.RequestId }, createdRequest);
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "Error creating the request: {ErrorMessage}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating the request.");
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
    }
}
