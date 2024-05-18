using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.Domain.DTOs;
using webapi.Domain.Enumerators;
using webapi.Domain.Models;
using webapi.Domain.Services;

namespace webapi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IBusinessService _businessService;
        private readonly IZakaziUserService _userService;
        private readonly IRequestService _requestService;

        public PostsController(IPostService postService, IBusinessService businessService, IZakaziUserService ZakaziUserService, IRequestService requestService)
        {
            _postService = postService;
            _businessService = businessService;
            _userService = ZakaziUserService;
            _requestService = requestService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {
            try
            {
                var posts = await _postService.GetAllPosts();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving posts from the database.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostById(int id)
        {
            try
            {
                var post = await _postService.GetPostById(id);
                if (post == null)
                {
                    return NotFound();
                }
                return Ok(post);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving the post.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            try
            {
                var createdPost = await _postService.CreatePost(post);
                return CreatedAtAction(nameof(GetPostById), new { id = createdPost.PostId }, createdPost);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating the post.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, Post post)
        {
            if (id != post.PostId)
            {
                return BadRequest();
            }

            try
            {
                await _postService.UpdatePost(post);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the post.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                // Call the RequestService to delete requests with the given PostId
                //await _requestService.DeleteRequestsByPostId(id);

                // Then delete the post
                await _postService.DeletePost(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the post.");
            }
        }



        [HttpGet("BetweenTimestamps")]
        public async Task<ActionResult<IEnumerable<OfferDto>>> GetPostsBetweenTimestamps(DateTime startTimestamp, DateTime endTimestamp)
        {
            try
            {
                var toReturn = new List<OfferDto>();
                var posts = await _postService.GetPostsBetweenTimestamps(startTimestamp, endTimestamp);

                foreach (var post in posts)
                {
                    // Fetch business information
                    var business = await _businessService.GetBusinessById(post.BusinessId);

                    // Fetch user information from the business
                    var user = await _userService.GetUserById(business.OwnerId.ToString());

                    // Create an OfferDto for each Post and fill in the missing elements
                    var offerDto = new OfferDto
                    {
                        PostId = post.PostId,
                        BusinessName = business?.BusinessName,
                        NameOfService = post.NameOfService,
                        Price = post.Price,
                        AvailabilityFrom = post.AvailabilityFrom,
                        AvailabilityTo = post.AvailabilityTo,
                        Name = user?.Name,
                        Surname = user?.Surname,
                        Email = user?.Email,
                        PhoneNumber = user?.PhoneNumber,
                        BusinessId = post.BusinessId
                    };

                    toReturn.Add(offerDto);
                }

                return Ok(toReturn);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving posts between timestamps.");
            }
        }



        [HttpGet("GetMyPosts/{id}")]
        public async Task<ActionResult<IEnumerable<OfferDto>>> GetMyPosts(int id)
        {
            try
            {
                var toReturn = new List<OfferDto>();
                var posts = await _postService.GetPostsByUserId(id);
               


                foreach (var post in posts)
                {
                    // Fetch business information
                    var business = await _businessService.GetBusinessById(post.BusinessId);

                    // Fetch user information from the business
                    var user = await _userService.GetUserById(business.OwnerId.ToString());

                    // Create an OfferDto for each Post and fill in the missing elements
                    var offerDto = new OfferDto
                    {
                        PostId = post.PostId,
                        BusinessName = business?.BusinessName,
                        NameOfService = post.NameOfService,
                        Price = post.Price,
                        AvailabilityFrom = post.AvailabilityFrom,
                        AvailabilityTo = post.AvailabilityTo,
                        Name = user?.Name,
                        Surname = user?.Surname,
                        Email = user?.Email,
                        PhoneNumber = user?.PhoneNumber,
                        BusinessId = post.BusinessId
                    };

                    toReturn.Add(offerDto);
                }
                return Ok(toReturn);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving posts from the database.");
            }
        }


    }
}
