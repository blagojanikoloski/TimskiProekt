using System.Threading.Tasks;
using webapi.Domain.Models;

namespace webapi.Domain.Services
{
    public interface IBusinessService
    {
        Task<Business> GetBusinessById(int businessId);
        // Add more methods as needed for managing businesses
    }
}
