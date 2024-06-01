using System.Threading.Tasks;
using webapi.Domain.Models;

namespace webapi.Domain.Services
{
    public interface IBusinessService
    {
        Task<Business> GetBusinessById(int businessId);
        Task<Business> CreateBusinessAsync(Business entity);
        Task<IEnumerable<Business>> GetBusinessesByOwnerIdAsync(int ownerId);
        Task<IEnumerable<Business>> GetAllBusinesses();
        Task<Business> DeleteBusinessAsync(Business business);
        // Add more methods as needed for managing businesses
    }
}
