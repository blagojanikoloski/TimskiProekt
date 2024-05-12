namespace webapi.Domain.Models;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using webapi.Domain.Enumerators;

public class Request
{
    public int RequestId { get; set; }

    [Required]
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;

    [Required]
    public RequestStatus RequestStatus { get; set; }

    [Required]
    public int PostId {  get; set; }

    [Required]
    public int BusinessId { get; set; }

    [Required]
    public int ClientId { get; set; }



}
