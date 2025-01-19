namespace Libarary_System.Models
{
    public class Member : User
    {
        public int MemberId { get; set; }
        public DateTime MembershipDate { get; set; } = DateTime.Now;
        public MemberStatus Status { get; set; }
        public List<Borrowing_Transaction> borrowing_Transactions { get; set; } = new List<Borrowing_Transaction>();
        public enum MemberStatus
        {
            Active,
            Inactive
        }
    }
}
