namespace TokanPages.Backend.Core.Utilities.DataUtilityService
{
    using System;
    using System.IO;
    using System.Net;

    public abstract class DataUtilityObject
    {
        public abstract DateTime GetRandomDateTime(DateTime? min = null, DateTime? max = null, int defaultYear = 2020);

        public abstract T GetRandomEnum<T>();

        public abstract int GetRandomInteger(int min = 0, int max = 12);

        public abstract decimal GetRandomDecimal(int min = 0, int max = 9999);

        public abstract MemoryStream GetRandomStream(int sizeInKb = 12);

        public abstract string GetRandomEmail(int length = 12, string domain = "gmail.com");

        public abstract string GetRandomString(int length = 12, string prefix = "", bool useAlphabetOnly = false);

        public abstract IPAddress GetRandomIpAddress(bool shouldReturnIPv6 = false);
    }
}