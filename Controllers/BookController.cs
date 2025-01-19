using Libarary_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Libarary_System.Mapping;
using Microsoft.EntityFrameworkCore;
using Libarary_System.Interfaces;
using Libarary_System.Dtos.BookDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace Libarary_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [Authorize]
        [HttpGet]
        public async  Task<IActionResult> GetAll([FromQuery] QueriesBook queriesBook)
        {
            var books = await _bookRepository.GetAll(queriesBook);
            return Ok(books);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookRepository.GetByIdDto(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [Authorize(Roles = "Admin,Staff")] 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookCreatDto book)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var newBook = book.ToBookCreateDto();

            await _bookRepository.Create(newBook);

            return await GetById(newBook.BookId);
        }
        
        [Authorize(Roles = "Admin,Staff")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto newbook)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var BooktoUpdate = await _bookRepository.GetBook(id);

            if (BooktoUpdate == null)
                return NotFound();
            
            await _bookRepository.RemoveAuthorBook(BooktoUpdate.Author);

            await _bookRepository.Update(BooktoUpdate, newbook);

            return  await GetById(id);
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {          
            return await _bookRepository.Delete(id)!= null ? Ok() : NotFound();
        }
    }
}
