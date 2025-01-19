using Libarary_System.Dtos.BorrowingTransactionDto;
using Libarary_System.Interfaces;
using Libarary_System.Mapping;
using Libarary_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Libarary_System.Repository
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly LibraryContext _context;
        public BorrowingRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Borrowing_Transaction borrowing_Transaction)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == borrowing_Transaction.BookId);
            book.AvailableCopies--;
            await _context.Borrowing_Transactions.AddAsync(borrowing_Transaction);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<BorrowViewDto>> GetAll()
        {
            var borrowing = await _context.Borrowing_Transactions.Include(b => b.Book).Include(b => b.Member).Include(b => b.Staff).ToListAsync();
            return borrowing.Select(b => b.ToBorrowingView()).ToList();
        }

        public async Task<Borrowing_Transaction?> GetById(int id)
        {
            return await _context.Borrowing_Transactions.FirstOrDefaultAsync(b => b.Borrowing_TransactionId == id);
        }

        public async Task<BorrowViewDto?> GetByIdDto(int id)
        {
            var borrowing = await _context.Borrowing_Transactions.Include(b => b.Book).Include(b => b.Member).Include(b => b.Staff).FirstOrDefaultAsync(b => b.Borrowing_TransactionId == id);
            if (borrowing == null)
            {
                return null;
            }
            return borrowing.ToBorrowingView();
        }

        public async Task<int> Update(Borrowing_Transaction borrowing, BorrowingUpdateDto borrowingUpdateDto)
        {
            borrowing.BorrowDate = borrowingUpdateDto.BorrowDate;
            borrowing.ActualReturnDate = borrowingUpdateDto.ActualReturnDate;
            borrowing.status = borrowingUpdateDto.status;
            if (borrowing.BookId != borrowingUpdateDto.BookId)
            {
                var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == borrowing.BookId);

                book.AvailableCopies++;

                book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == borrowingUpdateDto.BookId);

                book.AvailableCopies--;
            }
            borrowing.BookId = borrowingUpdateDto.BookId;
            borrowing.MemberId = borrowingUpdateDto.MemberId;
            borrowing.StaffId = borrowingUpdateDto.StaffId;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var borrowing = await GetById(id);
            if (borrowing == null)
            {
                return 0;
            }
            _context.Borrowing_Transactions.Remove(borrowing);
            return await _context.SaveChangesAsync();
        }
    }
}
