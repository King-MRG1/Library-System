using Libarary_System.Dtos.PublisherDto;
using Libarary_System.Interfaces;
using Libarary_System.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Libarary_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;
        public PublisherController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet]
        public  async Task<IActionResult> GetPublishers()
        {
            var publishers = await _publisherRepository.GetAllPublishers();
            return Ok(publishers);
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisher(int id)
        {
            var publisher = await _publisherRepository.GetPublisherByIdDto(id);
            
            if (publisher == null)
                return NotFound();

            return Ok(publisher);
        }

        //[Authorize(Roles = "Admin,Staff")]
        [HttpPost]
        public async Task<IActionResult> AddPublisher(PublisherCreateDto publisherCreate)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisher = publisherCreate.ToPublisherCreate();

             await _publisherRepository.AddPublisher(publisher);

            return await GetPublisher(publisher.PublisherId);
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, PublisherUpdateDto publisherUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisher = await _publisherRepository.GetPublisherById(id);

            if (publisher == null)
                return NotFound();

             await _publisherRepository.UpdatePublisher(publisherUpdate, publisher);

            return await GetPublisher(id);
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var publisher = await _publisherRepository.GetPublisherById(id);

            if (publisher == null)
                return NotFound();

            await _publisherRepository.DeletePublisher(id);

            return Ok();
        }
    }
}
