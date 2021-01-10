using DataLayer.Entities;
using Server.DTOs;
using System.Threading.Tasks;

namespace Server.Services
{
    public interface IAuthService
    {
        Task<AuthDto> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(Patient patient);
    }
}
