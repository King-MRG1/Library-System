using System.ComponentModel.DataAnnotations.Schema;

namespace Libarary_System.Models
{
    public class Borrowing_Transaction
    {
        public int Borrowing_TransactionId { get; set; }
        public DateTime BorrowDate { get; set; }= DateTime.Now;
        public DateTime ReturnDate { get; set; }
        public DateTime ActualReturnDate { get; set; }
        public Status status { get; set; }
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Book Book { get; set; }
        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        public Member Member { get; set; }
        [ForeignKey(nameof(Staff))]
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
        public enum Status
        {
            Borrowed,
            Returned
        }
    }
}
