using Microsoft.AspNetCore.Identity;

namespace webapi.Domain.Models
{
    public class UserRole : IdentityUserRole<int>
    {
        public ZakaziUser User { get; set; }
        public Role Role { get; set; }
    }
}
