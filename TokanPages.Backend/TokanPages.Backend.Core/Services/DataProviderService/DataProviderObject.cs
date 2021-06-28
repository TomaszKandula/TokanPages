using System;
using System.IO;
using System.Security.Claims;

namespace TokanPages.Backend.Core.Services.DataProviderService
{
    public abstract class DataProviderObject
    {
        public abstract DateTime GetRandomDateTime(DateTime? AMin = null, DateTime? AMax = null, int ADefaultYear = 2020);

        public abstract T GetRandomEnum<T>();

        public abstract int GetRandomInteger(int AMin = 0, int AMax = 12);

        public abstract decimal GetRandomDecimal(int AMin = 0, int AMax = 9999);

        public abstract MemoryStream GetRandomStream(int ASizeInKb = 12);

        public abstract string GetRandomEmail(int ALength = 12, string ADomain = "gmail.com");

        public abstract string GetRandomString(int ALength = 12, string APrefix = "");

        public abstract string GenerateJwt(DateTime AExpires, ClaimsIdentity AClaimsIdentity, string AWebSecret, string AIssuer, string ATargetAudience);
    }
}