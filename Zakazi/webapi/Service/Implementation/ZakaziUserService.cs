using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using webapi.Domain.Models;


namespace webapi.Domain.Services
{
    public class ZakaziUserService : IZakaziUserService
    {
        private readonly UserManager<ZakaziUser> _userManager;

        public ZakaziUserService(UserManager<ZakaziUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ZakaziUser> GetUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        // Implement more methods as needed for managing user information
    }
}
