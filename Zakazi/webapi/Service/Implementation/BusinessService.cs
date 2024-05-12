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

        public async Task<Business> CreateBusinessAsync(int userId, string businessName)
        {
            try
            {
                // Assuming you have a DbSet<Business> named Businesses in your DbContext
                var business = new Business
                {
                    BusinessName = businessName,
                    OwnerId = userId // Assuming the user ID is the owner ID
                };

                _context.Businesses.Add(business);
                await _context.SaveChangesAsync();

                return business;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw new Exception("Error creating business.", ex);
            }
        }

        // Implement more methods as needed for managing businesses
    }
}
