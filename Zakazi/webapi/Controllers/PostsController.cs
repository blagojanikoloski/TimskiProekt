using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.Domain.Models;
using webapi.Domain.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
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
                await _postService.DeletePost(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the post.");
            }
        }


        [HttpGet("GetMyPosts/{id}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetMyPosts(string id)
        {
            try
            {
                var posts = await _postService.GetPostsByWorkerId(id);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving posts from the database.");
            }
        }

    }
}
