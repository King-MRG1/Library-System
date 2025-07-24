using Microsoft.AspNetCore.Mvc;
using Libarary_System.Mapping;
using Libarary_System.Interfaces;
using Libarary_System.Dtos.BookDto;
using Microsoft.AspNetCore.Authorization;
using Libarary_System.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace Libarary_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IDistributedCache _cache;
        public BookController(IBookRepository bookRepository,IDistributedCache cache)
        {
            _bookRepository = bookRepository;
            _cache = cache;
        }
        //[Authorize]
        [HttpGet]
        public async  Task<IActionResult> GetAll([FromQuery] QueriesBook queriesBook)
        {
           
           string LoadLocation = "";

            string recordkey = $"Books_{queriesBook.publisherName}_{queriesBook.AtuhorName}_{queriesBook.SortBy}_{queriesBook.Isdescending}";

            var books = await _cache.GetRecordAsync<List<BookViewDtos>>(recordkey);

            if(books is null)
            {
                books = await _bookRepository.GetAll(queriesBook);

                LoadLocation = "Database";

                await _cache.SetRecordAsync(recordkey, books, TimeSpan.FromSeconds(60));
            }
            else
            {
                LoadLocation = "Cache";
            }

            Response.Headers.Append("Load-Location", LoadLocation);

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
