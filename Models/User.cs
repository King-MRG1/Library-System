using System.ComponentModel.DataAnnotations;

namespace Libarary_System.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? UserName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
