namespace webapi.Domain.Models;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class Client : IdentityUser
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
    public override string Email { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    // Password property is inherited from IdentityUser

}