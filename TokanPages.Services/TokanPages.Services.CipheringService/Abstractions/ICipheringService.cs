using TokanPages.Services.CipheringService.Helpers;

namespace TokanPages.Services.CipheringService.Abstractions;

public interface ICipheringService
{
    string GetHashedPassword(string password, string salt);

    bool VerifyPassword(string plaintext, string hashedPassword);

    string GenerateSalt(int logRounds = Constants.GenerateSaltDefaultLog2Rounds);
}