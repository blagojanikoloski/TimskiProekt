namespace webapi.Domain.DTOs
{
    public class PostDto
    {
        public int PostId { get; set; }
        public string NameOfService { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        // Add other properties as needed
    }

}
