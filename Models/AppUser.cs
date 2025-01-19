using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Libarary_System.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
