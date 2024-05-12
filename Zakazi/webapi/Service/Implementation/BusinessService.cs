using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.Domain.Models;
using webapi.Repository;

namespace webapi.Domain.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly DataContext _context;

        public BusinessService(DataContext context)
        {
            _context = context;
        }

        public async Task<Business> GetBusinessById(int businessId)
        {
            return await _context.Businesses.FirstOrDefaultAsync(b => b.BusinessId == businessId);
        }

        // Implement more methods as needed for managing businesses
    }
}
