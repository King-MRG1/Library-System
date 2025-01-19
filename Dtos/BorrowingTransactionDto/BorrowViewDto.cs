using Libarary_System.Models;
using static Libarary_System.Models.Borrowing_Transaction;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libarary_System.Dtos.BorrowingTransactionDto
{
    public class BorrowViewDto
    {
        public int Borrowing_TransactionId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ActualReturnDate { get; set; }
        public string status { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
    }
}
