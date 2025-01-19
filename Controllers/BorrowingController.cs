using Libarary_System.Dtos.BorrowingTransactionDto;
using Libarary_System.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Libarary_System.Mapping;
using Libarary_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;


namespace Libarary_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IBorrowingRepository _repositoryBorrowing;
        public BorrowingController(IBorrowingRepository repositoryBorrowing)
        {
            _repositoryBorrowing = repositoryBorrowing;
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var borrowing = await _repositoryBorrowing.GetAll();

            return Ok(borrowing);
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var borrowing = await _repositoryBorrowing.GetByIdDto(id);

            if (borrowing == null)
                return NotFound();

            return Ok(borrowing);
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BorrowingCreateDto borrowingCreateDto)
        {
            if (!ModelState.IsValid)
              return BadRequest(ModelState);
            
            var newBorrowing = borrowingCreateDto.ToBorrowing();

            await _repositoryBorrowing.Create(newBorrowing);

            return await GetById(newBorrowing.Borrowing_TransactionId);
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BorrowingUpdateDto borrowingUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var borrowing = await _repositoryBorrowing.GetById(id);

            if (borrowing == null)
                return NotFound();
           
            await _repositoryBorrowing.Update(borrowing, borrowingUpdateDto);

            return await GetById(id);
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _repositoryBorrowing.Delete(id) > 0 ? Ok() : NotFound();
        }

    }
}