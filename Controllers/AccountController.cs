using Libarary_System.Dtos.AccountDto;
using Libarary_System.Interfaces;
using Libarary_System.Models;
using Libarary_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Libarary_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManger;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IStaffRepository _staffRepositry;
        private readonly IMemberRepository _memberRepository;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager,IStaffRepository staffRepository, IMemberRepository memberRepository)
        {
            _userManger = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _staffRepositry = staffRepository;
            _memberRepository = memberRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("RegisterStaff")]
        public async Task<IActionResult> RegisterStaff([FromBody] RegisterStaffDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                    return BadRequest(new { Errors = errors });
                }

                var appuser = new AppUser
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                    Address = registerDto.Address,
                    PhoneNumber = registerDto.PhoneNumber
                };
                var result = await _userManger.CreateAsync(appuser, registerDto.Password);

                if (result.Succeeded)
                {
                    var user = await _userManger.AddToRoleAsync(appuser, "Staff");

                    if (user.Succeeded)
                    {
                        var staff = new Staff
                        {
                            FirstName = registerDto.FirstName,
                            LastName = registerDto.LastName,
                            Address = registerDto.Address,
                            PhoneNumber = registerDto.PhoneNumber,
                            NationalNumber = registerDto.NationalId,
                            StaffRole = (Staff.Role)registerDto.StaffRole,
                            HireDate = DateTime.Now,
                            borrowing_Transactions = new List<Borrowing_Transaction>(),
                            UserName = registerDto.UserName,
                            Email = registerDto.Email
                        };
                        await _staffRepositry.AddStaff(staff);
                        
                        return Ok(new UserDto
                            {
                                UserName = registerDto.UserName,
                                Email = registerDto.Email,
                                Token = await _tokenService.CreateToken(appuser)
                            });
                    }
                    else
                        return StatusCode(500, user.Errors);
                }
                else
                    return StatusCode(500, result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                    return BadRequest(new { Errors = errors });
                }
                var appuser = new AppUser
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                    Address = registerDto.Address,
                    PhoneNumber = registerDto.PhoneNumber
                };
                var result = await _userManger.CreateAsync(appuser, registerDto.Password);
                if (result.Succeeded)
                {
                    var user = await _userManger.AddToRoleAsync(appuser, "Admin");
                    if (user.Succeeded)
                    {
                        return Ok(new UserDto
                        {
                            UserName = registerDto.UserName,
                            Email = registerDto.Email,
                            Token = await _tokenService.CreateToken(appuser)
                        });
                    }
                    else
                        return StatusCode(500, user.Errors);
                }
                else
                    return StatusCode(500, result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [Authorize(Roles= "Staff,Admin")]
        [HttpPost("RegisterMember")]
        public async Task<IActionResult> RegisterMember([FromBody] RegisterMemberDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                    return BadRequest(new { Errors = errors });
                }
                
                var appuser = new AppUser
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                    Address = registerDto.Address,
                    PhoneNumber = registerDto.PhoneNumber
                };
                
                var result = await _userManger.CreateAsync(appuser, registerDto.Password);
                
                if (result.Succeeded)
                {
                    var user = await _userManger.AddToRoleAsync(appuser, "Member");
                    
                    if (user.Succeeded)
                    {
                        var member = new Member
                        {
                            FirstName = registerDto.FirstName,
                            LastName = registerDto.LastName,
                            Address = registerDto.Address,
                            PhoneNumber = registerDto.PhoneNumber,
                            MembershipDate = DateTime.Now,
                            UserName = registerDto.UserName,
                            Email = registerDto.Email,
                            borrowing_Transactions = new List<Borrowing_Transaction>(),
                        };

                        await _memberRepository.AddMember(member);

                        return Ok(new UserDto
                        {
                            UserName = registerDto.UserName,
                            Email = registerDto.Email,
                            Token = await _tokenService.CreateToken(appuser)
                        });
                    }
                    
                    else
                        return StatusCode(500, user.Errors);
                }
                else
                    return StatusCode(500, result.Errors);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManger.Users.FirstOrDefaultAsync(x => x.UserName == login.UserName);

            if (user == null)
                return Unauthorized("Invalid Ussername");

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded)
                return Unauthorized("User not found or password is incorrect");

            await _signInManager.SignInAsync(user, true);

            return Ok(
                new UserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = await _tokenService.CreateToken(user)
                });

        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}