using Libarary_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Libarary_System.Dtos.StaffDto
{
    public class StaffUpdateDto
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
        public string NationalId { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
