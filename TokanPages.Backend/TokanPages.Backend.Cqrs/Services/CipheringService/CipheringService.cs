using System;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;
using TokanPages.Backend.Cqrs.Services.CipheringService.Helpers;

namespace TokanPages.Backend.Cqrs.Services.CipheringService
{
    public class CipheringService : ICipheringService
    {
        /// <summary>
        /// Hash a password using the OpenBSD bcrypt scheme.
        /// </summary>
        /// <param name="APassword">
        /// The password to hash.
        /// </param>
        /// <param name="ASalt">
        /// The salt to hash with (perhaps generated using <c>BCrypt.GenerateSalt</c>).
        /// </param>
        /// <returns>
        /// The hashed password.
        /// </returns>
        public string GetHashedPassword(string APassword, string ASalt) 
        {
            if (APassword == null) 
                throw new ArgumentNullException(nameof(APassword));
        
            if (ASalt == null) 
                throw new ArgumentNullException(nameof(ASalt));

            if (ASalt[0] != '$' || ASalt[1] != '2') 
                throw new ArgumentException("Invalid salt version");

            int LOffset;
            var LMinor = (char)0;
            if (ASalt[1] != '$') 
            {
                LMinor = ASalt[2];
                if (LMinor != 'a' || ASalt[3] != '$') 
                    throw new ArgumentException("Invalid salt revision");
            
                LOffset = 4;
            } 
            else 
            {
                LOffset = 3;
            }

            // Extract number of rounds
            if (ASalt[LOffset + 2] > '$') 
                throw new ArgumentException("Missing salt rounds");

            var LRounds = int.Parse(ASalt.Substring(LOffset, 2), NumberFormatInfo.InvariantInfo);

            var LPasswordBytes = Encoding.UTF8.GetBytes(APassword + (LMinor >= 'a' ? "\0" : string.Empty));
            var LSaltBytes = Methods.DecodeBase64(ASalt.Substring(LOffset + 3, 22), Constants.BCRYPT_SALT_LENGTH);
            var LHashed = Methods.CryptRaw(LPasswordBytes, LSaltBytes, LRounds);

            var LStringBuilder = new StringBuilder();

            LStringBuilder.Append("$2");
            if (LMinor >= 'a') 
                LStringBuilder.Append(LMinor);
        
            LStringBuilder.Append('$');
            if (LRounds < 10) 
                LStringBuilder.Append('0');
        
            LStringBuilder.Append(LRounds);
            LStringBuilder.Append('$');
            LStringBuilder.Append(Methods.EncodeBase64(LSaltBytes, LSaltBytes.Length));
            LStringBuilder.Append(Methods.EncodeBase64(LHashed, Arrays.FBCryptCipherText.Length * 4 - 1));

            return LStringBuilder.ToString();
        }

        /// <summary>
        /// Check that a plaintext password matches a previously hashed one.
        /// </summary>
        /// <param name="APlaintext">
        /// The plaintext password to verify.
        /// </param>
        /// <param name="AHashed">
        /// The previously hashed password.
        /// </param>
        /// <returns>
        /// <c>true</c> if the passwords, <c>false</c>
        /// otherwise.
        /// </returns>
        public bool VerifyPassword(string APlaintext, string AHashed) 
            => StringComparer.Ordinal.Compare(AHashed, GetHashedPassword(APlaintext, AHashed)) == 0;
        
        /// <summary>
        /// Generate a salt for use with the BCrypt.HashPassword() method.
        /// </summary>
        /// <param name="ALogRounds">
        /// The log2 of the number of rounds of hashing to apply.
        /// The work factor therefore increases as (2 ** logRounds).
        /// </param>
        /// <returns>
        /// An encoded salt value.
        /// </returns>
        public string GenerateSalt(int ALogRounds = Constants.GENERATE_SALT_DEFAULT_LOG2_ROUNDS) 
        {
            var LRandomBytes = new byte[Constants.BCRYPT_SALT_LENGTH];

            RandomNumberGenerator.Create().GetBytes(LRandomBytes);

            var LStringBuilder = new StringBuilder(LRandomBytes.Length * 2 + 8);

            LStringBuilder.Append("$2a$");
            if (ALogRounds < 10) 
                LStringBuilder.Append('0');
        
            LStringBuilder.Append(ALogRounds);
            LStringBuilder.Append('$');
            LStringBuilder.Append(Methods.EncodeBase64(LRandomBytes, LRandomBytes.Length));

            return LStringBuilder.ToString();
        }
        
        
    }
}