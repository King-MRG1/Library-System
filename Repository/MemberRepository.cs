using Libarary_System.Dtos.MemberDto;
using Libarary_System.Interfaces;
using Libarary_System.Mapping;
using Libarary_System.Models;
using Libarary_System.Querires;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Libarary_System.Models.Borrowing_Transaction;
using static Libarary_System.Models.Member;

namespace Libarary_System.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LibraryContext _context;
        private readonly UserManager<AppUser> _userManager;
        public MemberRepository(LibraryContext context , UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<int> AddMember(Member member)
        {
          await _context.Members.AddAsync(member);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteMember(string username)
        {
            var member = _context.Members.Where(m => m.UserName.ToLower() == username.ToLower()).SingleOrDefault();
            
            if (member == null)
                return 0;

            _context.Members.Remove(member);

            return await _context.SaveChangesAsync();
        }

        public Task<Member?> GetMember(int id)
        {
            return _context.Members.Include(s=>s.borrowing_Transactions).SingleOrDefaultAsync(m=>m.MemberId==id);
        }

        public async Task<MemberViewDto?> GetMemberByEmail(string email)
        {
            return await _context.Members.Include(m => m.borrowing_Transactions).ThenInclude(b => b.Book).Where(m => m.Email.ToLower() == email.ToLower()).Select(m => m.ToMemberViewDto()).SingleOrDefaultAsync();
        }

        public async Task<MemberViewDto?> GetMemberByUsernameDto(string username)
        {
            return await _context.Members.Include(m=>m.borrowing_Transactions).ThenInclude(b=>b.Book).Where(m => m.UserName.ToLower() == username.ToLower()).Select(m => m.ToMemberViewDto()).SingleOrDefaultAsync();
        }

        public async Task<MemberViewDto?> GetMemberDto(int id)
        {
            return await _context.Members.Include(m => m.borrowing_Transactions).ThenInclude(b => b.Book).Where(m=>m.MemberId == id).Select(m => m.ToMemberViewDto()).SingleOrDefaultAsync();
        }

        public async Task<List<MemberViewDto>> GetMembers(MemberQueries memberQueries)
        {
            var members = _context.Members.Include(m=>m.borrowing_Transactions).ThenInclude(b=>b.Book).AsQueryable();

            if (!string.IsNullOrEmpty(memberQueries.FirstName))
            {
                members = members.Where(m => m.FirstName.ToLower().Contains(memberQueries.FirstName.ToLower()));
            }

            else if (!string.IsNullOrEmpty(memberQueries.LastName))
            {
                members = members.Where(m => m.LastName.ToLower().Contains(memberQueries.LastName.ToLower()));
            }

            else if (!string.IsNullOrEmpty(memberQueries.Address))
            {
                members = members.Where(m => m.Address.ToLower().Contains(memberQueries.Address.ToLower()));
            }

            else if (memberQueries.InActive != null)
            {
                members = members.Where(m => m.Status == (MemberStatus)memberQueries.InActive);
            }

            else if (!string.IsNullOrWhiteSpace(memberQueries.SortBy))
            {
                if (memberQueries.SortBy.Equals("First Name", StringComparison.OrdinalIgnoreCase))
                {
                    members = memberQueries.IsDescending ? members.OrderByDescending(s => s.FirstName) : members.OrderBy(s => s.FirstName);
                }

                else if (memberQueries.SortBy.Equals("Last Name", StringComparison.OrdinalIgnoreCase))
                {
                    members = memberQueries.IsDescending ? members.OrderByDescending(s => s.LastName) : members.OrderBy(s => s.LastName);
                }

                else if (memberQueries.SortBy.Equals("Membership Date", StringComparison.OrdinalIgnoreCase))
                {
                    members = memberQueries.IsDescending ? members.OrderByDescending(s => s.MembershipDate) : members.OrderBy(s => s.MembershipDate);
                }

            }

            return await members.Select(m=>m.ToMemberViewDto()).ToListAsync();
        }

        public async Task<int> UpdateMember(MemberUpdateDto memberUpdate, Member member)
        {
            var appuser = await _userManager.FindByEmailAsync(member.Email);

            if (appuser == null) 
                return 0;

            appuser.UserName = memberUpdate.UserName;
            appuser.FirstName = memberUpdate.FirstName;
            appuser.LastName = memberUpdate.LastName;
            appuser.PhoneNumber = memberUpdate.PhoneNumber;
            appuser.Address = memberUpdate.Address;

            member.UserName = memberUpdate.UserName;
            member.FirstName = member.FirstName;
            member.LastName = member.LastName;
            member.PhoneNumber = member.PhoneNumber;
            member.Address = memberUpdate.Address;

            return await _context.SaveChangesAsync();
        }

        public async Task<Member?> GetMemberByUserName(string username)
        {
            return await _context.Members.Where(m => m.UserName.ToLower() == username.ToLower()).FirstOrDefaultAsync();
        }
    }
}
