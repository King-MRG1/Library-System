namespace Libarary_System.Dtos.MemberDto
{
    public class MemberViewDto
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime MembershipDate { get; set; }
        public List<string> Borrowing { get; set; } = new List<string>();
    }
}
