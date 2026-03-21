using System.Security.Cryptography;

namespace auth_validation_ibckfv.helpers;

public static class PasswordHasher
{
    private const int _saltSize = 16;
    private const int _keySize = 32;
    private const int _iterations = 10000;
    private const char _saltDelimiter = ';';

    public static (string hash, string salt) Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);
        using var derive = new Rfc2898DeriveBytes(password, salt, _iterations, HashAlgorithmName.SHA256);
        byte[] key = derive.GetBytes(_keySize);
        return (Convert.ToBase64String(key), Convert.ToBase64String(salt));
    }

    public static bool Validate(string password, string storedHash, string storedSalt)
    {
        byte[] salt = Convert.FromBase64String(storedSalt);
        using var derive = new Rfc2898DeriveBytes(password, salt, _iterations, HashAlgorithmName.SHA256);
        byte[] key = derive.GetBytes(_keySize);
        return CryptographicOperations.FixedTimeEquals(key, Convert.FromBase64String(storedHash));
    }
}