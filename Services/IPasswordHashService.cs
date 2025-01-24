using MeteoLink.Data.Models;

namespace MeteoLink.Services
{
    public interface IPasswordHashService
    {
        string HashPassword(UserModel user, string password);
        bool VerifyPassword(UserModel user, string password, string hashedPassword);
    }
}
