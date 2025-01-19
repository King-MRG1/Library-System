using System.ComponentModel.DataAnnotations;
using static Libarary_System.Models.Borrowing_Transaction;

namespace Libarary_System.Dtos.BorrowingTransactionDto
{
    public class BorrowingUpdateDto
    {
        [Required(ErrorMessage = "The Borro Date is Required")]
        [DataType(DataType.Date)]
        public DateTime BorrowDate { get; set; }
        [Required(ErrorMessage = "The Return Date is Required")]
        [DataType(DataType.Date)]
        public DateTime ActualReturnDate { get; set; }
        [Required(ErrorMessage = "The Status of the borrowing is Required")]
        public Status status { get; set; }
        [Required(ErrorMessage = "The Book Id is Required")]
        public int BookId { get; set; }
        [Required(ErrorMessage = "The Member Id is Required")]
        public int MemberId { get; set; }
        [Required(ErrorMessage = "The Staff Id is Required")]
        public int StaffId { get; set; }
    }
}
