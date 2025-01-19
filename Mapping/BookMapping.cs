using Libarary_System.Dtos.BookDto;
using Libarary_System.Models;

namespace Libarary_System.Mapping
{
    public static class BookMapping
    {
        public static BookViewDtos ToBookView(this Book bookmodel)
        {
            return new BookViewDtos
            {
                Id = bookmodel.BookId,
                Tilte = bookmodel.Title,
                ISBN = bookmodel.ISBN,
                PublicationYear = bookmodel.PublicationYear,
                AvailableCopies = bookmodel.AvailableCopies,
                CategoryName = bookmodel.Category.CategoryName,
                PublisherName = bookmodel.Publisher.FirstName + " " + bookmodel.Publisher.LastName,
                AuthorsName = bookmodel.Author.Where(a => a.BookId == bookmodel.BookId).Select(Author => Author.Author.FirstName + " " + Author.Author.LastName).ToList()
            };
        }
        public static Book ToBookCreateDto(this BookCreatDto bookmodel)
        {
            return new Book
            {
                ISBN = bookmodel.ISBN,
                Title = bookmodel.Title,
                PublicationYear = bookmodel.PublicationYear,
                TotalCopies = bookmodel.TotalCopies,
                AvailableCopies = bookmodel.TotalCopies,
                PublisherId = bookmodel.PublisherId,
                CategoryId = bookmodel.CategoryId,
                Author = bookmodel.AuthorIds.Select(a => new Book_Author { AuthorId = a }).ToList(),
            };
        }
    }
}
