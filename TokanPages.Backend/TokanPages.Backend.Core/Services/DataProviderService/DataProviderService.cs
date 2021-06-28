using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace TokanPages.Backend.Core.Services.DataProviderService
{
    public class DataProviderService : DataProviderObject, IDataProviderService
    {
        private readonly Random FRandom;

        public DataProviderService() => FRandom = new Random();

        public override DateTime GetRandomDateTime(DateTime? AMin = null, DateTime? AMax = null, int ADefaultYear = 2020)
        {
            AMin ??= new DateTime(ADefaultYear, 1, 1); 
            AMax ??= new DateTime(ADefaultYear, 12, 31); 

            var LDayRange = (AMax - AMin).Value.Days; 

            return AMin.Value.AddDays(FRandom.Next(0, LDayRange));
        }

        public override T GetRandomEnum<T>()
        {
            var LValues = Enum.GetValues(typeof(T)); 
            return (T)LValues.GetValue(FRandom.Next(LValues.Length));
        }

        public override int GetRandomInteger(int AMin = 0, int AMax = 12) => FRandom.Next(AMin, AMax + 1);

        public override decimal GetRandomDecimal(int AMin = 0, int AMax = 9999) => FRandom.Next(AMin, AMax);

        public override MemoryStream GetRandomStream(int ASizeInKb = 12) => new (GetRandomByteArray(ASizeInKb));
        
        public override string GetRandomEmail(int ALength = 12, string ADomain = "gmail.com") => $"{GetRandomString(ALength)}@{ADomain}";

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

        public override string GenerateJwt(DateTime AExpires, ClaimsIdentity AClaimsIdentity, string AWebSecret)
        {
            var LTokenHandler = new JwtSecurityTokenHandler();
            var LKey = Encoding.ASCII.GetBytes(AWebSecret);
            var LTokenDescriptor = new SecurityTokenDescriptor
            {
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