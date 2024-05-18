using Microsoft.EntityFrameworkCore;
using webapi.Domain.Models;
using webapi.Repository.Interface;

namespace webapi.Repository.Implementation
{
    public class RequestRepository : IRequestRepository
    {
        private readonly DataContext _dataContext;

        public RequestRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<IEnumerable<Request>> GetRequestsByClientId(int clientId)
        {
            return await _dataContext.Requests
                .Include(requset => requset.Business)
                .Include(requset => requset.Posts)
                .Where(r => r.ClientId == clientId)
                .ToListAsync();
        }
    }
}
