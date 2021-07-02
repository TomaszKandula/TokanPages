using TokanPages.Backend.Cqrs.Services.Cipher.Helpers;

namespace TokanPages.Backend.Cqrs.Services.Cipher
{
    public interface ICipher
    {
        string GetHashedPassword(string APassword, string ASalt);

        bool VerifyPassword(string APlaintext, string AHashed);

        string GenerateSalt(int ALogRounds = CipherConstants.GENERATE_SALT_DEFAULT_LOG2_ROUNDS);
    }
}