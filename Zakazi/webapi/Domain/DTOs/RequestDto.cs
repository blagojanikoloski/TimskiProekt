using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using webapi.Domain.Enumerators;
using webapi.Domain.Models;

namespace webapi.Domain.DTOs
{
    public class RequestDto
    {
        public int RequestId { get; set; }
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;

        public RequestStatus RequestStatus { get; set; }

        public ICollection<Post> Posts { get; set; } = new Collection<Post>();

        public DateTimeOffset From { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset To { get; set; } = DateTimeOffset.UtcNow;

        public string BusinessName { get; set; } = String.Empty;
        public string ClientFirstName { get; set; } = String.Empty;
        public string ClientLastName { get; set; } = String.Empty;

        public double Price { get; set; }
    }
}
