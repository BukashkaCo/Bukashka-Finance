using System.Threading.Tasks;
using BukashkaCo.Finance.Domain.Models;

namespace BukashkaCo.Finance.Domain.Abstraction
{
    public interface IAuthService
    {
        Task<UserModel> Login(LoginRequest request);
    }
}