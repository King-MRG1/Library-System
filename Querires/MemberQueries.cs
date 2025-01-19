using static Libarary_System.Models.Member;
namespace Libarary_System.Querires
{
    public class MemberQueries
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public int? InActive { get; set; }
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; } = false;

    }
}
