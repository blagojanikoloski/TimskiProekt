using System.Threading.Tasks;
using webapi.Domain.Models;

namespace webapi.Domain.Services
{
    public interface IZakaziUserService
    {
        Task<ZakaziUser> GetUserById(string userId);
        // Add more methods as needed for managing user information
    }
}
