using System.ComponentModel.DataAnnotations;

namespace webapi.Domain.DTOs
{
    public class EditProfileDto
    {
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
