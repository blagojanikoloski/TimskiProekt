using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.Domain.Models;
using webapi.Repository;

namespace webapi.Domain.Services
{
    public class RequestService : IRequestService
    {
        private readonly DataContext _context;

        public RequestService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Request>> GetAllRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetRequestsByBusinessId(int id)
        {
            try
            {
                return await _context.Requests.Where(r => r.BusinessId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Request> CreateRequest(Request request)
        {
            try
            {
                _context.Requests.Add(request);
                await _context.SaveChangesAsync();
                return request;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw new Exception("Error creating the request.", ex);
            }
        }

        public async Task<Request> GetRequestById(int id)
        {
            return await _context.Requests.FindAsync(id);
        }

        public async Task UpdateRequest(Request request)
        {
            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRequest(int id)
        {
            var requestToDelete = await _context.Requests.FindAsync(id);
            if (requestToDelete != null)
            {
                _context.Requests.Remove(requestToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Request>> GetRequestsByClientId(int clientId)
        {
            return await _context.Requests.Where(r => r.ClientId == clientId).ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetRequestsByWorkerId(int workerId)
        {
            var requests = await (from req in _context.Requests
                                  join bus in _context.Businesses on req.BusinessId equals bus.BusinessId
                                  where bus.OwnerId == workerId
                                  select req).ToListAsync();

            return requests;
        }

        public async Task DeleteRequestsByPostId(int postId)
        {
            // Find requests with the given PostId
            var requestsToDelete = await _context.Requests.Where(r => r.PostId == postId).ToListAsync();

            if (requestsToDelete != null && requestsToDelete.Any())
            {
                // Remove the requests
                _context.Requests.RemoveRange(requestsToDelete);
                await _context.SaveChangesAsync();
            }
        }

    }
}
