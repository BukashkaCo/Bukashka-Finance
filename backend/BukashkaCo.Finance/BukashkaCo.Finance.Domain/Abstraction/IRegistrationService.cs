using System.Threading.Tasks;
using BukashkaCo.Finance.Domain.Models;

namespace BukashkaCo.Finance.Domain.Abstraction
{
    public interface IRegistrationService
    {
        Task<UserModel> Register(RegisterRequest request);
    }
}