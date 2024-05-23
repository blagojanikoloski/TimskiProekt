using System.Threading.Tasks;
using webapi.Domain.Models;

namespace webapi.Domain.Services
{
    public interface IBusinessService
    {
        Task<Business> GetBusinessById(int businessId);
        Task<Business> CreateBusinessAsync(int userId, string businessName);

        Task<IEnumerable<Business>> GetBusinessesByOwnerIdAsync(int ownerId);
        IEnumerable<Business> GetAllBusinesses();
        Task<Business> DeleteBusinessAsync(int businessId);
        // Add more methods as needed for managing businesses
    }
}
