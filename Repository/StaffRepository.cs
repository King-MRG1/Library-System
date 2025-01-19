using Libarary_System.Dtos.StaffDto;
using Libarary_System.Interfaces;
using Libarary_System.Mapping;
using Libarary_System.Models;
using Libarary_System.Querires;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Libarary_System.Repository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly LibraryContext _context;
        private readonly UserManager<AppUser> _userManager;
        public StaffRepository(LibraryContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<int> AddStaff(Staff staff)
        {
            await _context.Staff.AddAsync(staff);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteStaff(string username)
        {
            var staff = await _context.Staff.Where(s => s.UserName.ToLower() == username.ToLower()).SingleOrDefaultAsync();

            if (staff == null)
                return 0;

            _context.Staff.Remove(staff);

            return await _context.SaveChangesAsync();
        }

        public async Task<StaffViewDto?> GetStaffDto(int id)
        {
            var staff = await _context.Staff.Include(s => s.borrowing_Transactions).ThenInclude(b => b.Book).Where(s => s.StaffId == id).Select(s => s.ToStaffViewDto()).SingleOrDefaultAsync();
            if (staff == null)
                return null;
            return staff;
        }

        public async Task<StaffViewDto?> GetStaffByUsername(string username)
        {
            return await _context.Staff.Include(s => s.borrowing_Transactions).ThenInclude(b => b.Book).Where(s => s.UserName.ToLower() == username.ToLower()).Select(s => s.ToStaffViewDto()).SingleOrDefaultAsync();
        }

        public async Task<StaffViewDto?> GetStaffByEmail(string email)
        {
            return await _context.Staff.Include(s => s.borrowing_Transactions).ThenInclude(b => b.Book).Where(s => s.Email.ToLower() == email.ToLower()).Select(s => s.ToStaffViewDto()).SingleOrDefaultAsync();
        }

        public async Task<StaffViewDto?> GetStaffByNationalId(string nationalId)
        {
            return await _context.Staff.Include(s => s.borrowing_Transactions).ThenInclude(b => b.Book).Where(s => s.NationalNumber == nationalId).Select(s => s.ToStaffViewDto()).SingleOrDefaultAsync();
        }

        public async Task<List<StaffViewDto>> GetStaffs(QueriesStaff staffQueries)
        {
            var staffs = _context.Staff.Include(s => s.borrowing_Transactions).ThenInclude(b => b.Book).AsQueryable();

            if (!string.IsNullOrWhiteSpace(staffQueries.Firstname))
            {
                staffs = staffs.Where(s => s.FirstName.Contains(staffQueries.Firstname));
            }

            if (!string.IsNullOrWhiteSpace(staffQueries.Lastname))
            {
                staffs = staffs.Where(s => s.LastName.Contains(staffQueries.Lastname));
            }

            if (!string.IsNullOrWhiteSpace(staffQueries.Address))
            {
                staffs = staffs.Where(s => s.Address.Contains(staffQueries.Address));
            }

            if (!string.IsNullOrWhiteSpace(staffQueries.SortBy))
            {
                if (staffQueries.SortBy.Equals("First Name", StringComparison.OrdinalIgnoreCase))
                {
                    staffs = staffQueries.IsDescending ? staffs.OrderByDescending(s => s.FirstName) : staffs.OrderBy(s => s.FirstName);
                }

                else if (staffQueries.SortBy.Equals("Last Name", StringComparison.OrdinalIgnoreCase))
                {
                    staffs = staffQueries.IsDescending ? staffs.OrderByDescending(s => s.LastName) : staffs.OrderBy(s => s.LastName);
                }

                else if (staffQueries.SortBy.Equals("role", StringComparison.OrdinalIgnoreCase))
                {
                    staffs = staffQueries.IsDescending ? staffs.OrderByDescending(s => s.StaffRole.ToString()) : staffs.OrderBy(s => s.StaffRole.ToString());
                }

                else if (staffQueries.SortBy.Equals("hire Date", StringComparison.OrdinalIgnoreCase))
                {
                    staffs = staffQueries.IsDescending ? staffs.OrderByDescending(s => s.HireDate) : staffs.OrderBy(s => s.HireDate);
                }
            }

            return await staffs.Select(s => s.ToStaffViewDto()).ToListAsync();
        }

        public async Task<int> UpdateStaff(StaffUpdateDto updateDto, Staff staff)
        {
            var appUser = await _userManager.FindByEmailAsync(staff.Email);
            if (appUser == null)
                return 0;

            appUser.UserName = updateDto.UserName;
            appUser.FirstName = updateDto.FirstName;
            appUser.LastName = updateDto.LastName;
            appUser.PhoneNumber = updateDto.PhoneNumber;
            appUser.Address = updateDto.Address;

            staff.FirstName = updateDto.FirstName;
            staff.LastName = updateDto.LastName;
            staff.UserName = updateDto.UserName;
            staff.Address = updateDto.Address;
            staff.PhoneNumber = updateDto.PhoneNumber;
            staff.NationalNumber = updateDto.NationalId;

            return await _context.SaveChangesAsync();
        }

        public async Task<Staff?> GetStaff(int id)
        {
            return await _context.Staff.Include(s => s.borrowing_Transactions).SingleOrDefaultAsync(s => s.StaffId == id);
        }
    }
}
