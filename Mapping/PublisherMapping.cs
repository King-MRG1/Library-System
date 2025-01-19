using Libarary_System.Dtos.PublisherDto;
using Libarary_System.Models;

namespace Libarary_System.Mapping
{
    public static class PublisherMapping
    {
        public static PublisherViewDto ToPublisherViewDto(this Publisher publisher)
        {
            return new PublisherViewDto
            {
                Name = publisher.FirstName+ "  " +publisher.LastName,
                Id = publisher.PublisherId,
                Email = publisher.Email,
                Address = publisher.Address,
                PhoneNumber = publisher.PhoneNumber,
                Books = publisher.Books.Where(b=>b.PublisherId == publisher.PublisherId).Select(b => b.Title).ToList()
            };
        }
        public static Publisher ToPublisherCreate(this PublisherCreateDto publisherCreateDto)
        {
            return new Publisher
            {
                FirstName = publisherCreateDto.FirstName,
                LastName = publisherCreateDto.LastName,
                Email = publisherCreateDto.Email,
                Address = publisherCreateDto.Address,
                PhoneNumber = publisherCreateDto.PhoneNumber,
                Books = new List<Book>()
            };
        }
    }
}
