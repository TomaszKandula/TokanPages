namespace TokanPages.Backend.Shared.Services.DataUtilityService
{
    using System;
    using System.IO;
    using System.Net;

    public abstract class DataUtilityObject
    {
        public abstract DateTime GetRandomDateTime(DateTime? AMin = null, DateTime? AMax = null, int ADefaultYear = 2020);

        public abstract T GetRandomEnum<T>();

        public abstract int GetRandomInteger(int AMin = 0, int AMax = 12);

        public abstract decimal GetRandomDecimal(int AMin = 0, int AMax = 9999);

        public abstract MemoryStream GetRandomStream(int ASizeInKb = 12);

        public abstract string GetRandomEmail(int ALength = 12, string ADomain = "gmail.com");

        public abstract string GetRandomString(int ALength = 12, string APrefix = "");

        public abstract IPAddress GetRandomIpAddress(bool AShouldReturnIPv6 = false);
    }
}