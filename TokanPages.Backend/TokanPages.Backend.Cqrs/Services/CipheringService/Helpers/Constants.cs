namespace TokanPages.Backend.Cqrs.Services.CipheringService.Helpers;

public static class Constants
{
    public const int GenerateSaltDefaultLog2Rounds = 10;
    
    public const int CryptSaltLength = 16;

    public const int BlowfishNumRounds = 16;
}