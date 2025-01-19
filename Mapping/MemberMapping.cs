using Libarary_System.Dtos.MemberDto;
using Libarary_System.Models;

namespace Libarary_System.Mapping
{
    public static class MemberMapping
    {
        public static MemberViewDto ToMemberViewDto(this Member member)
        {
            return new MemberViewDto
            {
                MemberId = member.MemberId,
                Name = member.FirstName + " " + member.LastName,
                UserName = member.UserName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,
                Address = member.Address,
                Status = member.Status.ToString(),
                Borrowing = member.borrowing_Transactions.Where(b => b.MemberId == member.MemberId).Select(b => b.Book.Title).ToList()
            };
        }
    }
}