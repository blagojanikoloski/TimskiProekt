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
    public float Price { get; set; }

    [Required]
    public DateTime AvailabilityFrom { get; set; }

    [Required]
    public DateTime AvailabilityTo { get; set; }
}
