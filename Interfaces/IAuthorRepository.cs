using Libarary_System.Dtos.AuthorDto;
using Libarary_System.Models;

namespace Libarary_System.Interfaces
{
    public interface IAuthorRepository
    {
        public Task<List<AuthorViewDto>> GetAll();
        public Task<AuthorViewDto?> GetByIdDto(int id);
        public Task<int> AddAuthor(Author author);
        public Task<Author?> GetById(int id);
        public Task<int> UpdateAuthor( AuthorUpdateDto authorUpdate , Author author);
        public Task<int?> DeleteAuthor(int id);
        public Task<AuthorViewDto?> GetByNationality(int nationality);
    }
}
