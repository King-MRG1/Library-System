using Libarary_System.Dtos.AuthorDto;
using Libarary_System.Interfaces;
using Libarary_System.Mapping;
using Libarary_System.Models;
using Microsoft.EntityFrameworkCore;
using static Libarary_System.Models.Author;

namespace Libarary_System.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _context;
        public AuthorRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<int> AddAuthor(Author author)
        {
            await _context.Authors.AddAsync(author);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> DeleteAuthor(int id)
        {
            var author = await GetById(id);

            if (author == null)
            return null;

            _context.Authors.Remove(author);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<AuthorViewDto>> GetAll()
        {
            return await _context.Authors.Include(a => a.book_Authors).ThenInclude(a => a.Book).Select(a => a.ToAuthorViewDto()).ToListAsync();
        }

        public async Task<AuthorViewDto?> GetByNationality(int nationality)
        {
            return await _context.Authors.Include(a => a.book_Authors).ThenInclude(a => a.Book)
                .Where(a => a.Nationality == (Nationalit)nationality).Select(a => a.ToAuthorViewDto()).FirstOrDefaultAsync();
        }

        public async Task<Author?> GetById(int id)
        {
            return await _context.Authors.Include(a => a.book_Authors).ThenInclude(a => a.Book).SingleOrDefaultAsync(a => a.AuthorId == id);
        }

        public async Task<AuthorViewDto?> GetByIdDto(int id)
        {
            return await _context.Authors.Include(a => a.book_Authors).ThenInclude(a => a.Book).Where(a => a.AuthorId == id).Select(a => a.ToAuthorViewDto()).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAuthor(AuthorUpdateDto authorUpdate,Author author)
        {
            foreach(var book in author.book_Authors)
            {
                _context.Books_Author.Remove(book);
            }

            author.FirstName = authorUpdate.FirstName;
            author.LastName = authorUpdate.LastName;
            author.Email = authorUpdate.Email;
            author.Nationality = (Nationalit)authorUpdate.Nationality;
            author.Biography = authorUpdate.Biography;
            author.book_Authors = authorUpdate.BookIds.Select(b => new Book_Author { BookId = b }).ToList();

            return await _context.SaveChangesAsync();
        }
    }
}
