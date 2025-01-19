using Libarary_System.Dtos.PublisherDto;
using Libarary_System.Models;

namespace Libarary_System.Interfaces
{
    public interface IPublisherRepository
    {
        public Task<List<PublisherViewDto>> GetAllPublishers();
        public Task<PublisherViewDto?> GetPublisherByIdDto(int id);
        public Task<Publisher?> GetPublisherById(int id);
        public Task<int> AddPublisher(Publisher publisher);
        public Task<int> UpdatePublisher(PublisherUpdateDto publisherUpdate,Publisher publisher);
        public Task<int> DeletePublisher(int id);
    }
}
