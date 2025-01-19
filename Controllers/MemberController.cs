using Libarary_System.Dtos.MemberDto;
using Libarary_System.Interfaces;
using Libarary_System.Models;
using Libarary_System.Querires;
using Libarary_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Libarary_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Staff")]

    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly UserManager<AppUser> _userManager;
        public MemberController(IMemberRepository memberRepository, UserManager<AppUser> userManager)
        {
            _memberRepository = memberRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers([FromQuery] MemberQueries memberQueries)
        {
            var members = await _memberRepository.GetMembers(memberQueries);
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMember(int id)
        {
            var member = await _memberRepository.GetMemberDto(id);

            if (member == null)
                return NotFound();

            return Ok(member);
        }

        [HttpGet("UserName")]
        public async Task<IActionResult> GetMemberByUsername([Required] string username)
        {
            var restult = await _userManager.FindByNameAsync(username);

            if (restult == null)
            {
                return NotFound();
            }

            var member = await _memberRepository.GetMemberByUsernameDto(username);

            if (member == null)
                return NotFound();

            return Ok(member);
        }

        [HttpGet("Email")]
        public async Task<IActionResult> GetMemberByEmail([Required] string email)
        {
            var result = await _userManager.FindByEmailAsync(email);

            if (result == null)
                return NotFound();

            var member = await _memberRepository.GetMemberByEmail(email);

            if (member == null)
                return NotFound();

            return Ok(member);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateMember( int Id, [FromBody] MemberUpdateDto memberUpdate)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var member = await _memberRepository.GetMember(Id);

            if (member == null)
                return NotFound();
            
            var result = await _memberRepository.UpdateMember(memberUpdate, member);

            if (result == 0)
                return StatusCode(StatusCodes.Status500InternalServerError);
 
            return await GetMember(Id);
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteMember([Required] string username)
        {
            var member = await _memberRepository.GetMemberByUserName(username);
            
            if (member == null)
                return NotFound($"The Member {username} not found");
            
            var user = await _userManager.FindByEmailAsync(member.Email);
            
            if (user == null)
                return NotFound();
            
            var result = await _userManager.DeleteAsync(user);
            
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _memberRepository.DeleteMember(username);

            return Ok();
        }

    }
}
