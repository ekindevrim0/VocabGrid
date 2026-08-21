using System.Security.Cryptography;
using System.Text;

namespace VocabGrid.Services;

/// <summary>
/// HMAC-SHA512 password hashing, extracted from AuthController so
/// UserController's change-password endpoint doesn't duplicate
/// security-sensitive code.
/// </summary>
public static class PasswordHasher
{
    public static void CreateHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public static bool VerifyHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        if (passwordHash.Length == 0 || passwordSalt.Length == 0)
        {
            return false;
        }

        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }
}
