using Microsoft.AspNetCore.Identity;

namespace webapi.Domain.Models
{
    public class Role: IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
