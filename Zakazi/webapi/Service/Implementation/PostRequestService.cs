using System;
using webapi.Repository;
using webapi.Service.Interface;

namespace webapi.Service.Implementation
{
    public class PostRequestService : IPostRequestService
    {
        private readonly DataContext _context;

        public PostRequestService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<int>> GetPostIdsByRequestId(int requestId)
        {
            return null;
        }
    }
}
