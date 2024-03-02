namespace webapi.Domain.Models;

using System;
using System.ComponentModel.DataAnnotations;
using webapi.Domain.Enumerators;

public class Request
{
    public int RequestId { get; set; }

    [Required]
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;

    [Required]
    public RequestStatus RequestStatus { get; set; }

    [Required]
    public int PostId { get; set; }

    [Required]
    public string WorkerId { get; set; }

    [Required]
    public string ClientId { get; set; }

}
