using webapi.Domain.Models;

namespace webapi.Repository.InterFace
{
    public interface IBusinessRepository
    {
        Task<bool> SaveAllAsync();
        Task<IEnumerable<Business>> GetAllBusinessesAsync();
        Task<Business> GetBusinessByIdAsync(int businessId);
        Task<IEnumerable<Business>> GetBusinessesByOwnerIdAsync(int ownerId);
        Task<Business> Insert(Business entity);
        Task<Business> Update(Business entity);
        Task<Business> Delete(Business entity);
    }
}
