namespace webapi.Domain.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class Request
{
    public int RequestId { get; set; }

    [Required]
    public DateTime Timestamp { get; set; }

    [Required]
    public string Status { get; set; }

    [Required]
    public int PostId { get; set; }

    [Required]
    public string WorkerId { get; set; }

    [Required]
    public string ClientId { get; set; }

}
