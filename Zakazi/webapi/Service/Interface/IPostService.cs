﻿using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.Domain.Models;

namespace webapi.Domain.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPosts();

        Task<IEnumerable<Post>> GetPostsByWorkerId(string id);
        Task<Post> GetPostById(int id);
        Task<Post> CreatePost(Post post);
        Task UpdatePost(Post post);
        Task DeletePost(int id);
    }
}