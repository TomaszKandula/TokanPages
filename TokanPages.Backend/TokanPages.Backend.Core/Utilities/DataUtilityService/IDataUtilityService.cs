namespace TokanPages.Backend.Core.Utilities.DataUtilityService
{
    using System;
    using System.IO;
    using System.Net;

    public interface IDataUtilityService
    {
        DateTime GetRandomDateTime(DateTime? AMin = null, DateTime? AMax = null, int ADefaultYear = 2020);

        T GetRandomEnum<T>();

        int GetRandomInteger(int AMin = 0, int AMax = 12);

        decimal GetRandomDecimal(int AMin = 0, int AMax = 9999);

        MemoryStream GetRandomStream(int ASizeInKb = 12);

        string GetRandomEmail(int ALength = 12, string ADomain = "gmail.com");

        string GetRandomString(int ALength = 12, string APrefix = "", bool AUseAlphabetOnly = false);

        IPAddress GetRandomIpAddress(bool AShouldReturnIPv6 = false);
    }
}