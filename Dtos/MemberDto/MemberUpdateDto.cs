using System.ComponentModel.DataAnnotations;

namespace Libarary_System.Dtos.MemberDto
{
    public class MemberUpdateDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
