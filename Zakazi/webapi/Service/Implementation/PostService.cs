using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.Domain.Models;
using webapi.Repository;

namespace webapi.Domain.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _context;

        public PostService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<Post> CreatePost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task UpdatePost(Post post)
        {
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePost(int id)
        {
            var postToDelete = await _context.Posts.FindAsync(id);
            if (postToDelete != null)
            {
                _context.Posts.Remove(postToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Post>> GetPostsByBusinessId(int id)
        {
            try
            {
                return await _context.Posts.Where(r => r.BusinessId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<List<Post>> GetPostsByUserId(int userId)
        {
            return await _context.Posts.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task DeletePostsByBusinessId(int businessId)
        {
            var posts = await _context.Posts.Where(p => p.BusinessId == businessId).ToListAsync();
            _context.Posts.RemoveRange(posts);
            await _context.SaveChangesAsync();
        }

    }
}
