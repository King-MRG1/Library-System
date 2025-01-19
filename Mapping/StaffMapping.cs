using Libarary_System.Dtos.StaffDto;
using Libarary_System.Models;

namespace Libarary_System.Mapping
{
    public static class StaffMapping
    {
        public static StaffViewDto ToStaffViewDto(this Staff staff)
        {
            return new StaffViewDto
            {
                StaffId = staff.StaffId,
                Name = staff.FirstName+ " " + staff.LastName,
                RoleName = staff.StaffRole.ToString(),
                UserName = staff.UserName,
                Email = staff.Email,
                PhoneNumber = staff.PhoneNumber,
                HireDate = staff.HireDate,
                NationalId = staff.NationalNumber,
                Address = staff.Address,
                Borrowing = staff.borrowing_Transactions.Where(b=>b.StaffId == staff.StaffId).Select(b => b.Book.Title).ToList()
            };
        }
    }
}
