using Libarary_System.Dtos.StaffDto;
using Libarary_System.Interfaces;
using Libarary_System.Models;
using Libarary_System.Querires;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Libarary_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class StaffController : ControllerBase
    {
        private readonly IStaffRepository _staffRepositry;
        private readonly UserManager<AppUser> _userManager;
        public StaffController(IStaffRepository staffRepository,UserManager<AppUser> userManager)
        {
            _staffRepositry = staffRepository;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetStaffs([FromQuery] QueriesStaff staffQueries)
        {
            var staffs = await _staffRepositry.GetStaffs(staffQueries);
            return Ok(staffs);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(int id)
        {
            var staff = await _staffRepositry.GetStaffDto(id);
            if (staff == null)
                return NotFound();
            return Ok(staff);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("UserName")]
        public async Task<IActionResult> GetStaffByUserName([Required]string username)
        {
            var found= await _userManager.FindByNameAsync(username);

            if (found == null)
                return NotFound();

            var staff = await _staffRepositry.GetStaffByUsername(found.UserName);

            return Ok(staff);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Email")]
        public async Task<IActionResult> GetStaffByEmail([Required] string email)
        {
            var found = await _userManager.FindByEmailAsync(email);
            if (found == null)
                return NotFound();
            var staff = await _staffRepositry.GetStaffByEmail(found.Email);
            return Ok(staff);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("NationalId")]
        public async Task<IActionResult> GetStaffByNationalId([Required] string nationalId)
        {
            var staff = await _staffRepositry.GetStaffByNationalId(nationalId);
            if (staff == null)
                return NotFound();
            return Ok(staff);
        }
    
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff([Required] int id,[FromBody]StaffUpdateDto staffUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var staff = await _staffRepositry.GetStaff(id);

            if (staff == null)
                return NotFound();

            var result = await _staffRepositry.UpdateStaff(staffUpdate, staff);
            if (result == 0) return NotFound(result);
            return await GetStaffById(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteStaff( [Required] string username)
        {
            var staff = await _staffRepositry.GetStaffByUsername(username);
            
            if (staff == null)
                return NotFound($"The staff {username} not found");
            
            var user = await _userManager.FindByEmailAsync(staff.Email);
            
            if(user == null)
                return NotFound();
            
            var result = await _userManager.DeleteAsync(user);
            
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            
            await _staffRepositry.DeleteStaff(username);
            
            return Ok();
        }
    }
}
