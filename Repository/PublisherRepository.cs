using Libarary_System.Dtos.PublisherDto;
using Libarary_System.Interfaces;
using Libarary_System.Mapping;
using Libarary_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Libarary_System.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly LibraryContext _context;
        public PublisherRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<int> AddPublisher(Publisher publisher)
        {
            await _context.Publishers.AddAsync(publisher);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePublisher(int id)
        {
            var publisher = await _context.Publishers.SingleOrDefaultAsync(x => x.PublisherId == id);

             _context.Publishers.Remove(publisher);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<PublisherViewDto>> GetAllPublishers()
        {
            return await _context.Publishers.Include(p => p.Books).Select(p => p.ToPublisherViewDto()).ToListAsync();
        }

        public async Task<Publisher?> GetPublisherById(int id)
        {
            return await _context.Publishers.Include(p => p.Books).SingleOrDefaultAsync(x => x.PublisherId == id);
        }

        public async Task<PublisherViewDto?> GetPublisherByIdDto(int id)
        {
            return await _context.Publishers.Include(p => p.Books).Where(p=>p.PublisherId == id).Select(p => p.ToPublisherViewDto()).SingleOrDefaultAsync();
        }

        public async Task<int> UpdatePublisher(PublisherUpdateDto publisherUpdate, Publisher publisher)
        {

            publisher.FirstName = publisherUpdate.FirstName;
            publisher.LastName = publisherUpdate.LastName;
            publisher.Email = publisherUpdate.Email;
            publisher.Address = publisherUpdate.Address;
            publisher.PhoneNumber = publisherUpdate.PhoneNumber;

            return await _context.SaveChangesAsync();
        }
    }
}
