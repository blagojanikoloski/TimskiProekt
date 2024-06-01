using Microsoft.EntityFrameworkCore;
using webapi.Domain.Models;
using webapi.Repository.InterFace;

namespace webapi.Repository.Implementation
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly DataContext _dataContext;

        public BusinessRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Business> Delete(Business entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _dataContext.Businesses.Remove(entity);
            await SaveAllAsync();
            return entity;
        }

        public async Task<IEnumerable<Business>> GetAllBusinessesAsync()
        {
            return await _dataContext.Businesses
               //.Include(entity => entity.Employees)
               //.Include(entity => entity.Posts)
               //.Include(entity => entity.Requests)
               .ToListAsync();
        }

        public async Task<Business> GetBusinessByIdAsync(int businessId)
        {
            return await _dataContext.Businesses
                .Include(entity => entity.Employees)
                .Include(entity => entity.Posts)
                .Include(entity => entity.Requests)
                .SingleOrDefaultAsync(entity => entity.BusinessId == businessId);
        }

        public async Task<IEnumerable<Business>> GetBusinessesByOwnerIdAsync(int ownerId)
        {
            return await _dataContext.Businesses
                //.Include(entity => entity.Employees)
                //.Include(entity => entity.Posts)
                //.Include(entity => entity.Requests)
                .Where(entity => entity.OwnerId == ownerId)
                .ToListAsync();
        }

        public async Task<Business> Insert(Business entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _dataContext.Businesses.Add(entity);
            await SaveAllAsync();
            return entity;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<Business> Update(Business entity)
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
            await SaveAllAsync();
            return entity;
        }

}
}
