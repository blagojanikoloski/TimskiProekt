using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.Domain.Models;
using webapi.Repository;
using webapi.Repository.InterFace;

namespace webapi.Domain.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;

        public BusinessService(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }

        public async Task<Business> GetBusinessById(int businessId)
        {
            return await _businessRepository.GetBusinessByIdAsync(businessId);
        }

        public async Task<Business> CreateBusinessAsync(Business entity)
        {
            return await _businessRepository.Insert(entity);
        }

        public async Task<IEnumerable<Business>> GetBusinessesByOwnerIdAsync(int ownerId)
        {
            return await _businessRepository.GetBusinessesByOwnerIdAsync(ownerId);
        }

        public async Task<IEnumerable<Business>> GetAllBusinesses()
        {
            return await _businessRepository.GetAllBusinessesAsync();
        }

        public async Task<Business> DeleteBusinessAsync(Business business)
        {
            return await _businessRepository.Delete(business);
        }

        // Implement more methods as needed for managing businesses
    }
}
