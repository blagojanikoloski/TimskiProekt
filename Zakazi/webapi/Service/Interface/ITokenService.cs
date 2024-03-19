using webapi.Domain.Models;

namespace webapi.Service.Interface
{
    public interface ITokenService
    {
        string GenerateJwtToken(ZakaziUser user);
    }
}
