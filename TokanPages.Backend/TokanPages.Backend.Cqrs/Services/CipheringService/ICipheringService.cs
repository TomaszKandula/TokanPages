namespace TokanPages.Backend.Cqrs.Services.CipheringService
{
    using Helpers;

    public interface ICipheringService
    {
        string GetHashedPassword(string APassword, string ASalt);

        bool VerifyPassword(string APlaintext, string AHashedPassword);

        string GenerateSalt(int ALogRounds = Constants.GENERATE_SALT_DEFAULT_LOG2_ROUNDS);
    }
}