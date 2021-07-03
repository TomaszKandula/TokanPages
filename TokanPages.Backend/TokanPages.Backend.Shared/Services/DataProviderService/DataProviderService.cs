using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace TokanPages.Backend.Shared.Services.DataProviderService
{
    public class DataProviderService : DataProviderObject, IDataProviderService
    {
        private readonly Random FRandom;

        public DataProviderService() => FRandom = new Random();

        /// <summary>
        /// Returns randomized Date within given range.
        /// </summary>
        /// <remarks>
        /// It uses System.Random function, therefore it should not be used
        /// for security-critical applications or for protecting sensitive data.
        /// </remarks>>
        /// <param name="AMin">Minimum value of expected date. Value can be null (if so, default day and month is: 1 JAN).</param>
        /// <param name="AMax">Maximum value of expected date. Value can be null (if so, default day and month is: 31 DEC).</param>
        /// <param name="ADefaultYear">If not given, it uses 2020 year as default value.</param>
        /// <returns>New randomized date.</returns>
        public override DateTime GetRandomDateTime(DateTime? AMin = null, DateTime? AMax = null, int ADefaultYear = 2020)
        {
            AMin ??= new DateTime(ADefaultYear, 1, 1); 
            AMax ??= new DateTime(ADefaultYear, 12, 31); 

            var LDayRange = (AMax - AMin).Value.Days; 

            return AMin.Value.AddDays(FRandom.Next(0, LDayRange));
        }
        
        /// <summary>
        /// Returns randomized enumeration.
        /// </summary>
        /// <remarks>
        /// It uses System.Random function, therefore it should not be used
        /// for security-critical applications or for protecting sensitive data.
        /// </remarks>>
        /// <typeparam name="T">Given type.</typeparam>
        /// <returns>New randomized enumeration.</returns>
        public override T GetRandomEnum<T>()
        {
            var LValues = Enum.GetValues(typeof(T)); 
            return (T)LValues.GetValue(FRandom.Next(LValues.Length));
        }

        /// <summary>
        /// Returns randomized integer number within given range.
        /// </summary>
        /// <remarks>
        /// It uses System.Random function, therefore it should not be used
        /// for security-critical applications or for protecting sensitive data.
        /// </remarks>>
        /// <param name="AMin">A boundary value, lowest possible.</param>
        /// <param name="AMax">A boundary value, highest possible.</param>
        /// <returns>New randomized integer number.</returns>
        public override int GetRandomInteger(int AMin = 0, int AMax = 12) => FRandom.Next(AMin, AMax + 1);

        /// <summary>
        /// Returns randomized decimal number within given range.
        /// </summary>
        /// <remarks>
        /// It uses System.Random function, therefore it should not be used
        /// for security-critical applications or for protecting sensitive data.
        /// </remarks>>
        /// <param name="AMin">A boundary value, lowest possible.</param>
        /// <param name="AMax">A boundary value, highest possible.</param>
        /// <returns>New randomized decimal number.</returns>
        public override decimal GetRandomDecimal(int AMin = 0, int AMax = 9999) => FRandom.Next(AMin, AMax);

        /// <summary>
        /// Returns randomized stream of bytes.
        /// </summary>
        /// <remarks>
        /// It uses System.Random function, therefore it should not be used
        /// for security-critical applications or for protecting sensitive data.
        /// </remarks>>
        /// <param name="ASizeInKb">Expected size in kBytes.</param>
        /// <returns>New randomized stream of bytes.</returns>
        public override MemoryStream GetRandomStream(int ASizeInKb = 12) => new (GetRandomByteArray(ASizeInKb));

        /// <summary>
        /// Returns randomized e-mail address.
        /// </summary>
        /// <remarks>
        /// It uses System.Random function, therefore it should not be used
        /// for security-critical applications or for protecting sensitive data.
        /// </remarks>>
        /// <param name="ALength">Expected length of the name, 12 characters by default.</param>
        /// <param name="ADomain">Domain, "gmail.com" by default.</param>
        /// <returns>New randomized e-mail address.</returns>
        public override string GetRandomEmail(int ALength = 12, string ADomain = "gmail.com") => $"{GetRandomString(ALength)}@{ADomain}";

        /// <summary>
        /// Returns randomized string with given length and prefix (optional).
        /// </summary>
        /// <remarks>
        /// It uses System.Random function, therefore it should not be used
        /// for security-critical applications or for protecting sensitive data.
        /// </remarks>>
        /// <param name="ALength">Expected length, 12 characters by default.</param>
        /// <param name="APrefix">Optional prefix.</param>
        /// <returns>New randomized string.</returns>
        public override string GetRandomString(int ALength = 12, string APrefix = "")
        {
            if (ALength == 0) 
                return string.Empty; 

            const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; 

            var LString = new string(Enumerable.Repeat(CHARS, ALength)
                .Select(AString => AString[FRandom.Next(AString.Length)])
                .ToArray()); 

            if (!string.IsNullOrEmpty(APrefix)) 
                return APrefix.Trim() + LString; 

            return LString;
        }

        /// <summary>
        /// Returns a new security token with given claims and expiration date and time.
        /// </summary>
        /// <param name="AExpires">Value of the 'expiration' claim. This value should be in UTC.</param>
        /// <param name="AClaimsIdentity">Claims that will be used when creating a security token.</param>
        /// <param name="AWebSecret">String used to generate token key.</param>
        /// <param name="AIssuer">Issuer of a security token.</param>
        /// <param name="ATargetAudience">Target audience.</param>
        /// <returns>New JSON Web Token.</returns>
        public override string GenerateJwt(DateTime AExpires, ClaimsIdentity AClaimsIdentity, string AWebSecret, string AIssuer, string ATargetAudience)
        {
            var LTokenHandler = new JwtSecurityTokenHandler();
            var LKey = Encoding.ASCII.GetBytes(AWebSecret);
            var LTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = AIssuer,
                IssuedAt = DateTime.UtcNow,
                Audience = ATargetAudience,
                Subject = AClaimsIdentity,
                Expires = AExpires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(LKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var LToken = LTokenHandler.CreateToken(LTokenDescriptor);
            return LTokenHandler.WriteToken(LToken);
        }
        
        private byte[] GetRandomByteArray(int ASizeInKb = 12)
        {
            var LByteBuffer = new byte[ASizeInKb * 1024]; 
            FRandom.NextBytes(LByteBuffer); 
            return LByteBuffer;
        }
    }
}