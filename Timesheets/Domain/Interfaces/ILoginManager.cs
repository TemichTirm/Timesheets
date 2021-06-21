using System.Threading.Tasks;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Domain.Interfaces
{
    public interface ILoginManager
    {
        Task<LoginResponse> Authenticate(User user);
        Task<bool> CheckRefreshToken(string token);
        bool CheckRefreshTokenValidity(string token);
        Task DeleteRefreshToken(string token);
        Task<User> GetUser(string token);
    }
}
