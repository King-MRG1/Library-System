using System.ComponentModel.DataAnnotations;

namespace Libarary_System.Dtos.AuthorDto
{
    public class AuthorCreateDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Nationality { get; set; }
        [Required]
        [MinLength(30)]
        public string Biography { get; set; }
    }
}
