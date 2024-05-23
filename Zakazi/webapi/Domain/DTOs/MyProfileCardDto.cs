using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
        public string From { get; set; } 

        [Required]
        public string To { get; set; }
        [Required]
        public string Name {  get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string BusinessName {  get; set; }

        [Required]
        public string NameOfService { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string RequestStatusInString {  get; set; }
        [Required]
        public List<PostDto> Posts { get; set; }


    }
}
