using Libarary_System.Dtos.MemberDto;
using Libarary_System.Models;
using Libarary_System.Querires;

namespace Libarary_System.Interfaces
{
    public interface IMemberRepository
    {
        Task<List<MemberViewDto>> GetMembers(MemberQueries memberQueries);
        Task<MemberViewDto?> GetMemberDto(int id);
        Task<MemberViewDto?> GetMemberByUsernameDto(string username);
        Task<MemberViewDto?> GetMemberByEmail(string email);
        Task<int> AddMember(Member member);
        Task<int> UpdateMember(MemberUpdateDto memberUpdate, Member member);
        Task<int> DeleteMember(string username);
        Task<Member?> GetMember(int id);
        Task<Member?> GetMemberByUserName(string username);
    }
}
