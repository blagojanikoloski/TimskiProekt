namespace webapi.Domain.Models;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class ZakaziUser : IdentityUser<int>
{

    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
    public override string Email { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    // Can be null for clients
    public string? Profession { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    // Password property is inherited from IdentityUser
}
