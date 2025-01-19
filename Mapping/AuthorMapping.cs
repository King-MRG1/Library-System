using Libarary_System.Dtos.AuthorDto;
using Libarary_System.Models;

namespace Libarary_System.Mapping
{
    public static class AuthorMapping
    {
        public static AuthorViewDto ToAuthorViewDto(this Author author)
        {
            return new AuthorViewDto
            {
                AuthorId = author.AuthorId,
                Name = author.FirstName + " " + author.LastName,
                Emaill = author.Email,
                Biography = author.Biography,
                Nationality = author.Nationality.ToString(),
                Books = author.book_Authors.Where(a=>a.AuthorId == author.AuthorId).Select(ba => ba.Book.Title).ToList()
            };
        }
        public static Author ToAuthorCreateDto(this AuthorCreateDto author)
        {
            return new Author
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email = author.Email,
                Biography = author.Biography,
                book_Authors = new List<Book_Author>()
            };
        }
    }
}
