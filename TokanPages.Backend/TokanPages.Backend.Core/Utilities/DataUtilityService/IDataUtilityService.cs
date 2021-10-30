namespace TokanPages.Backend.Core.Utilities.DataUtilityService
{
    using System;
    using System.IO;
    using System.Net;

    public interface IDataUtilityService
    {
        DateTime GetRandomDateTime(DateTime? min = null, DateTime? max = null, int defaultYear = 2020);

        T GetRandomEnum<T>();

        int GetRandomInteger(int min = 0, int max = 12);

        decimal GetRandomDecimal(int min = 0, int max = 9999);

        MemoryStream GetRandomStream(int sizeInKb = 12);

        string GetRandomEmail(int length = 12, string domain = "gmail.com");

        string GetRandomString(int length = 12, string prefix = "", bool useAlphabetOnly = false);

        IPAddress GetRandomIpAddress(bool shouldReturnIPv6 = false);
    }
}