using MeteoLink.Data.Models;

namespace MeteoLink.Services
{
    public interface ITokenService
    {
        string CreateToken(UserModel user, string username);
    }
}
