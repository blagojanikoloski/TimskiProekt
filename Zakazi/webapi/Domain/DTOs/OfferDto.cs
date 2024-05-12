using System.ComponentModel.DataAnnotations;
using webapi.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace webapi.Domain.DTOs
{
    public class OfferDto
    {
        public int PostId { get; set; }
        public string BusinessName { get; set; }

        [Required]
        public string NameOfService { get; set; }

        [Required]
        public double Price { get; set; } = 0;

        [Required]
        public DateTimeOffset AvailabilityFrom { get; set; } = DateTimeOffset.UtcNow;

        [Required]
        public DateTimeOffset AvailabilityTo { get; set; } = DateTimeOffset.UtcNow;

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
