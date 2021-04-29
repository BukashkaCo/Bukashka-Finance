using BukashkaCo.Finance.Domain.Entities;

namespace BukashkaCo.Finance.Domain.Abstraction
{
    public interface IJwtGenerator
    {
        string CreateToken(User user);
    }
}