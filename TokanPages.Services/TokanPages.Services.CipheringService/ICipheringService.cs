namespace TokanPages.Services.CipheringService;

using Helpers;

public interface ICipheringService
{
    string GetHashedPassword(string password, string salt);

    bool VerifyPassword(string plaintext, string hashedPassword);

    string GenerateSalt(int logRounds = Constants.GenerateSaltDefaultLog2Rounds);
}