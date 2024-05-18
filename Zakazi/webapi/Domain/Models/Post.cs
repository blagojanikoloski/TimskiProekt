namespace webapi.Domain.Models;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Post
{
    public int PostId { get; set; }

    [Required]
    public int BusinessId { get; set; }

    [Required]
    public int UserId { get; set; } 

    [Required]
    public string NameOfService { get; set; }

    [Required]
    public double Price { get; set; } = 0;

    [Required]
    public DateTimeOffset AvailabilityFrom { get; set; } = DateTimeOffset.UtcNow;

    [Required]
    public DateTimeOffset AvailabilityTo { get; set; } = DateTimeOffset.UtcNow;

    // prevents dependency loop 
    [JsonIgnore]
    public ICollection<Request> Requests { get; set; } = new Collection<Request>();
}
