using APIDemo.Entities;

namespace APIDemo.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
