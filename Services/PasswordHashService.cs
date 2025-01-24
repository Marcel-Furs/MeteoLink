using MeteoLink.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace MeteoLink.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        private readonly PasswordHasher<UserModel> _passwordHasher;

        public PasswordHashService()
        {
            _passwordHasher = new PasswordHasher<UserModel>();
        }

        public string HashPassword(UserModel user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(UserModel user, string password, string hashedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
