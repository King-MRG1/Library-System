using Libarary_System.Dtos.BookDto;
using Libarary_System.Models;
namespace Libarary_System.Interfaces
{
    public interface IBookRepository
    {
        public Task<List<BookViewDtos>> GetAll(QueriesBook queriesBook);
        public Task<BookViewDtos?> GetByIdDto(int id);
        public Task<Book?> GetBook(int id);
        public Task<int> Create(Book book);
        public Task<int> Update(Book oldbook, UpdateBookDto newbook);
        public Task<int?> Delete(int id);
        public Task<int> RemoveAuthorBook(List<Book_Author> bookAuthor);
    }
}
