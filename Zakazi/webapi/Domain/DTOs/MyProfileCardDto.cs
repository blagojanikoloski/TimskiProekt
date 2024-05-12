using System.ComponentModel.DataAnnotations;
using webapi.Domain.Enumerators;

namespace webapi.Domain.DTOs
{
    public class MyProfileCardDto
    {

        [Required]
        public int RequestId { get; set; }

        [Required]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;

        [Required]
        public RequestStatus RequestStatus { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public int BusinessId { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public DateTimeOffset From { get; set; } = DateTimeOffset.UtcNow;

        [Required]
        public DateTimeOffset To { get; set; } = DateTimeOffset.UtcNow;
    }
}
