﻿using System.ComponentModel.DataAnnotations;

namespace Libarary_System.Dtos.AccountDto
{
    public class RegisterStaffDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        //[Unique("AspNetUsers")]
        public string UserName { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required,DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        //[Unique("Staff")]
        public string NationalId { get; set; }
        [Required]
        public int StaffRole { get; set; }

    }
}
