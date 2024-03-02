namespace webapi.Domain.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class Post
{
    public int PostId { get; set; }

    [Required]
    public string WorkerId { get; set; }

    [Required]
    public string NameOfService { get; set; }

    [Required]
    public double Price { get; set; } = 0;

    [Required]
    public DateTimeOffset AvailabilityFrom { get; set; } = DateTimeOffset.UtcNow;

    [Required]
    public DateTimeOffset AvailabilityTo { get; set; } = DateTimeOffset.UtcNow;
}
