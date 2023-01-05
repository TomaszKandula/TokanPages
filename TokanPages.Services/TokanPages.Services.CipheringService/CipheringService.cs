using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using TokanPages.Services.CipheringService.Abstractions;
using TokanPages.Services.CipheringService.Helpers;

namespace TokanPages.Services.CipheringService;

public sealed class CipheringService : ICipheringService
{
    /// <summary>
    /// Hash a password using the OpenBSD bcrypt scheme.
    /// </summary>
    /// <param name="password">
    /// The password to hash.
    /// </param>
    /// <param name="salt">
    /// The salt to hash with (perhaps generated using <c>BCrypt.GenerateSalt</c>).
    /// </param>
    /// <returns>
    /// The hashed password.
    /// </returns>
    public string GetHashedPassword(string password, string salt) 
    {
        if (password == null) 
            throw new ArgumentNullException(nameof(password));
        
        if (salt == null) 
            throw new ArgumentNullException(nameof(salt));

        if (salt[0] != '$' || salt[1] != '2') 
            throw new ArgumentException("Invalid salt version");

        int offset;
        var minor = (char)0;
        if (salt[1] != '$') 
        {
            minor = salt[2];
            if (minor != 'a' || salt[3] != '$') 
                throw new ArgumentException("Invalid salt revision");
            
            offset = 4;
        } 
        else 
        {
            offset = 3;
        }

        // Extract number of rounds
        if (salt[offset + 2] > '$') 
            throw new ArgumentException("Missing salt rounds");

        var rounds = int.Parse(salt.Substring(offset, 2), NumberFormatInfo.InvariantInfo);

        var passwordBytes = Encoding.UTF8.GetBytes(password + (minor >= 'a' ? "\0" : string.Empty));
        var saltBytes = Methods.DecodeBase64(salt.Substring(offset + 3, 22), Constants.CryptSaltLength);
        var hashed = Methods.CryptRaw(passwordBytes, saltBytes, rounds);

        var stringBuilder = new StringBuilder();

        stringBuilder.Append("$2");
        if (minor >= 'a') 
            stringBuilder.Append(minor);
        
        stringBuilder.Append('$');
        if (rounds < 10) 
            stringBuilder.Append('0');
        
        stringBuilder.Append(rounds);
        stringBuilder.Append('$');
        stringBuilder.Append(Methods.EncodeBase64(saltBytes, saltBytes.Length));
        stringBuilder.Append(Methods.EncodeBase64(hashed, Arrays.CryptCipherText.Length * 4 - 1));

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Check that a plaintext password matches a previously hashed one.
    /// </summary>
    /// <param name="plaintext">
    /// The plaintext password to verify.
    /// </param>
    /// <param name="hashedPassword">
    /// The previously hashed password.
    /// </param>
    /// <returns>
    /// <c>true</c> if the passwords, <c>false</c> otherwise.
    /// </returns>
    public bool VerifyPassword(string plaintext, string hashedPassword)
    {
        var getSaltFromHashedPassword = hashedPassword[..29];
        var getHashedPassword = GetHashedPassword(plaintext, getSaltFromHashedPassword);
        return getHashedPassword == hashedPassword;
    }
        
    /// <summary>
    /// Generate a salt for use with the BCrypt.HashPassword() method.
    /// </summary>
    /// <param name="logRounds">
    /// The log2 of the number of rounds of hashing to apply.
    /// The work factor therefore increases as (2 ** logRounds).
    /// </param>
    /// <returns>
    /// An encoded salt value.
    /// </returns>
    public string GenerateSalt(int logRounds = Constants.GenerateSaltDefaultLog2Rounds) 
    {
        var randomBytes = new byte[Constants.CryptSaltLength];

        RandomNumberGenerator.Create().GetBytes(randomBytes);

        var stringBuilder = new StringBuilder(randomBytes.Length * 2 + 8);

        stringBuilder.Append("$2a$");
        if (logRounds < 10) 
            stringBuilder.Append('0');
        
        stringBuilder.Append(logRounds);
        stringBuilder.Append('$');
        stringBuilder.Append(Methods.EncodeBase64(randomBytes, randomBytes.Length));

        return stringBuilder.ToString();
    }
}