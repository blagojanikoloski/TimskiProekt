using webapi.Domain.Models;

namespace webapi.Repository.Interface
{
    public interface IRequestRepository
    {
        Task<IEnumerable<Request>> GetRequestsByClientId(int clientId);
    }
}
