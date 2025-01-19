using Libarary_System.Models;

namespace Libarary_System.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user); 
    }
}
