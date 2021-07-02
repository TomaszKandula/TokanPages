namespace TokanPages.Backend.Cqrs.Services.Cipher.Helpers
{
    public static class CipherConstants
    {
        public const int GENERATE_SALT_DEFAULT_LOG2_ROUNDS = 10;
    
        public const int BCRYPT_SALT_LENGTH = 16;

        public const int BLOWFISH_NUM_ROUNDS = 16;
    }
}