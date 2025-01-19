using System.ComponentModel.DataAnnotations;

namespace Libarary_System.Dtos.PublisherDto
{
    public class PublisherCreateDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

    }
}
