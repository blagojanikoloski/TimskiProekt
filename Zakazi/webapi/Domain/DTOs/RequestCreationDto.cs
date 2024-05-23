namespace webapi.Domain.DTOs
{
    public class RequestCreationDto
    {
        // Other properties needed for creating a request
        public int BusinessId { get; set; }
        public int ClientId { get; set; }
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To { get; set; }

        // Collection of Post IDs
        public List<int> PostIds { get; set; }
    }

}
