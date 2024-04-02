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

        public async Task<IEnumerable<Request>> GetRequestsByClientIdOrWorkerId(string id)
        {
            try
            {
                return await _context.Requests.Where(r => r.ClientId == id || r.WorkerId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw; 
            }
        }

        public async Task<Request> CreateRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
            return request;
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
    }
}
