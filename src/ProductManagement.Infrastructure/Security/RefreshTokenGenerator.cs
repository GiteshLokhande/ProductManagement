using System.Security.Cryptography;

namespace ProductManagement.Infrastructure.Security
{
    public static class RefreshTokenGenerator
    {
        public static string Generate()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(randomBytes);
        }
    }
}
