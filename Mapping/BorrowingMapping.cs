using Libarary_System.Dtos.BorrowingTransactionDto;
using Libarary_System.Models;

namespace Libarary_System.Mapping
{
    public static class BorrowingMapping
    {
        public static BorrowViewDto ToBorrowingView(this Borrowing_Transaction borrowing_Transaction) => new BorrowViewDto
        {
            Borrowing_TransactionId = borrowing_Transaction.Borrowing_TransactionId,
            BorrowDate = borrowing_Transaction.BorrowDate,
            ActualReturnDate = borrowing_Transaction.ActualReturnDate,
            status = borrowing_Transaction.status.ToString(),
            BookName = borrowing_Transaction.Book.Title,
            BookId = borrowing_Transaction.Book.BookId,
            MemberName = borrowing_Transaction.Member.FirstName + " " + borrowing_Transaction.Member.LastName,
            MemberId = borrowing_Transaction.Member.MemberId,
            StaffName = borrowing_Transaction.Staff.FirstName + " " + borrowing_Transaction.Staff.LastName,
            StaffId = borrowing_Transaction.Staff.StaffId
        };
        public static Borrowing_Transaction ToBorrowing(this BorrowingCreateDto borrowing)
        {
            return new Borrowing_Transaction
            {
                BorrowDate = borrowing.BorrowDate,
                ActualReturnDate = borrowing.ActualReturnDate,
                BookId = borrowing.BookId,
                MemberId = borrowing.MemberId,
                StaffId = borrowing.StaffId,
                status = borrowing.status
            };
        }
    }
}
