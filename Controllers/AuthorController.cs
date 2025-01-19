using Libarary_System.Dtos.AuthorDto;
using Libarary_System.Interfaces;
using Libarary_System.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Libarary_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorRepository.GetAll();
            return Ok(authors);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _authorRepository.GetByIdDto(id);

            if (author == null)
                return NotFound();
            
            return Ok(author);
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreateDto authorcreate)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var author = authorcreate.ToAuthorCreateDto();

            await _authorRepository.AddAuthor(author);

            return await GetAuthor(author.AuthorId);
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorUpdateDto authorUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var authorToUpdate = await _authorRepository.GetById(id);

            if (authorToUpdate == null)
                return NotFound();

            await _authorRepository.UpdateAuthor(authorUpdate,authorToUpdate);

            return await GetAuthor(id);
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _authorRepository.GetById(id);

            if (author == null)
                return NotFound();

            await _authorRepository.DeleteAuthor(id);

            return NoContent();
        }

    }
}
