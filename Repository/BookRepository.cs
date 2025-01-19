using Libarary_System.Dtos.BookDto;
using Libarary_System.Interfaces;
using Libarary_System.Mapping;
using Libarary_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Libarary_System.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;
        public BookRepository(LibraryContext context)
        {
            _context = context;
        }
        public async Task<int> Create(Book book)
        {
            await _context.Books.AddAsync(book);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<BookViewDtos>> GetAll(QueriesBook queriesBook)
        {
            var Books = _context.Books.Include(b => b.Author).ThenInclude(ba => ba.Author).Include(b => b.Category).Include(b => b.Publisher).AsQueryable();

            if (!string.IsNullOrWhiteSpace(queriesBook.publisherName))
            {
                Books = Books.Where(b => (b.Publisher.FirstName + b.Publisher.LastName).Contains(queriesBook.publisherName));
            }

            if (!string.IsNullOrWhiteSpace(queriesBook.AtuhorName))
            {
                Books = Books.Where(b => b.Author.Any(ba => (ba.Author.FirstName+ba.Author.LastName).Contains(queriesBook.AtuhorName)));
            }

            if (!string.IsNullOrWhiteSpace(queriesBook.SortBy))
            {
                if (queriesBook.SortBy.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    Books = queriesBook.Isdescending ? Books.OrderByDescending(b => b.Title) : Books.OrderBy(b => b.Title);
                }
                else if (queriesBook.SortBy.Equals("publicationYear", StringComparison.OrdinalIgnoreCase))
                {
                    Books = queriesBook.Isdescending ? Books.OrderByDescending(b => b.PublicationYear) : Books.OrderBy(b => b.PublicationYear);
                }
                else if (queriesBook.SortBy.Equals("totalCopies", StringComparison.OrdinalIgnoreCase))
                {
                    Books = queriesBook.Isdescending ? Books.OrderByDescending(b => b.TotalCopies) : Books.OrderBy(b => b.TotalCopies);
                }
                else if (queriesBook.SortBy.Equals("availableCopies", StringComparison.OrdinalIgnoreCase))
                {
                    Books = queriesBook.Isdescending ? Books.OrderByDescending(b => b.AvailableCopies) : Books.OrderBy(b => b.AvailableCopies);
                }

            }

            return await Books.Select(b => b.ToBookView()).ToListAsync();
        }

        public async Task<BookViewDtos?> GetByIdDto(int id)
        {
            var book = await _context.Books.Include(b => b.Author).ThenInclude(ba => ba.Author).Include(b => b.Category).Include(b => b.Publisher).Where(b => b.BookId == id).Select(b => b.ToBookView()).SingleOrDefaultAsync();
            if (book == null)
            {
                return null;
            }
            return book;
        }

        public async Task<int> Update(Book oldbook, UpdateBookDto newbook)
        {

            oldbook.ISBN = newbook.ISBN;
            oldbook.Title = newbook.Title;
            oldbook.PublicationYear = newbook.PublicationYear;
            oldbook.TotalCopies = newbook.TotalCopies;
            oldbook.AvailableCopies = newbook.TotalCopies;
            oldbook.PublisherId = newbook.PublisherId;
            oldbook.CategoryId = newbook.CategoryId;
            foreach (var item in newbook.AuthorIds)
            {
                oldbook.Author.Add(new Book_Author { AuthorId = item, BookId = oldbook.BookId });
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveAuthorBook(List<Book_Author> bookAuthor)
        {
            foreach (var item in bookAuthor)
            {
                _context.Books_Author.Remove(item);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<Book?> GetBook(int id)
        {
            return await _context.Books.FindAsync(id) != null ? await _context.Books.Include(s => s.Author).FirstOrDefaultAsync(s => s.BookId == id) : null;
        }
    }
}
