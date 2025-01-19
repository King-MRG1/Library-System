using Libarary_System.Dtos.BorrowingTransactionDto;
using Libarary_System.Models;

namespace Libarary_System.Interfaces
{
    public interface IBorrowingRepository
    {
        public Task<List<BorrowViewDto>> GetAll();
        public Task<BorrowViewDto?> GetByIdDto(int id);
        public Task<int> Create(Borrowing_Transaction borrowing_Transaction);
        public Task<int> Update(Borrowing_Transaction borrowing, BorrowingUpdateDto borrowingUpdateDto);
        public Task<Borrowing_Transaction?>GetById(int id);
        public Task<int> Delete(int id);
    }
}
