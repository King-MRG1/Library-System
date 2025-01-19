using Libarary_System.Dtos.StaffDto;
using Libarary_System.Models;
using Libarary_System.Querires;

namespace Libarary_System.Interfaces
{
    public interface IStaffRepository
    {
        Task<List<StaffViewDto>> GetStaffs(QueriesStaff staffQueries);
        Task<StaffViewDto?> GetStaffDto(int id);
        Task<StaffViewDto?> GetStaffByUsername(string username);
        Task<StaffViewDto?> GetStaffByEmail(string email);
        Task<StaffViewDto?> GetStaffByNationalId(string nationalId);
        Task<int> AddStaff(Staff staff);
        Task<int> UpdateStaff(StaffUpdateDto staffUpdate,Staff staff);
        Task<int> DeleteStaff(string username);
        Task<Staff?>GetStaff(int id);
        
    }
}
