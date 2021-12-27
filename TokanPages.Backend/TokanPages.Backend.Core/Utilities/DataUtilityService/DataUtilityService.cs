namespace TokanPages.Backend.Core.Utilities.DataUtilityService;

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Security.Cryptography;

public class DataUtilityService : IDataUtilityService
{
    private readonly RandomNumberGenerator _numberGenerator;

    public DataUtilityService() => _numberGenerator = RandomNumberGenerator.Create();

    /// <summary>
    /// Returns randomized Date within given range.
    /// </summary>
    /// <remarks>
    /// It uses System.Random function, therefore it should not be used
    /// for security-critical applications or for protecting sensitive data.
    /// </remarks>
    /// <param name="min">Minimum value of expected date. Value can be null (if so, default day and month is: 1 JAN).</param>
    /// <param name="max">Maximum value of expected date. Value can be null (if so, default day and month is: 31 DEC).</param>
    /// <param name="defaultYear">If not given, it uses 2020 year as default value.</param>
    /// <returns>New randomized date.</returns>
    public virtual DateTime GetRandomDateTime(DateTime? min = null, DateTime? max = null, int defaultYear = 2020)
    {
        min ??= new DateTime(defaultYear, 1, 1); 
        max ??= new DateTime(defaultYear, 12, 31); 

        var daysRange = (max - min).Value.Days; 

        return min.Value.AddDays(RandomNext(0, daysRange));
    }
        
    /// <summary>
    /// Returns randomized enumeration.
    /// </summary>
    /// <remarks>
    /// It uses System.Random function, therefore it should not be used
    /// for security-critical applications or for protecting sensitive data.
    /// </remarks>
    /// <typeparam name="T">Given type.</typeparam>
    /// <returns>New randomized enumeration.</returns>
    public virtual T GetRandomEnum<T>()
    {
        var values = Enum.GetValues(typeof(T)); 
        return (T)values.GetValue(RandomNext(values.Length));
    }

    /// <summary>
    /// Returns randomized integer number within given range.
    /// </summary>
    /// <remarks>
    /// It uses System.Random function, therefore it should not be used
    /// for security-critical applications or for protecting sensitive data.
    /// </remarks>
    /// <param name="min">A boundary value, lowest possible.</param>
    /// <param name="max">A boundary value, highest possible.</param>
    /// <returns>New randomized integer number.</returns>
    public virtual int GetRandomInteger(int min = 0, int max = 12) => RandomNext(min, max + 1);

    /// <summary>
    /// Returns randomized decimal number within given range.
    /// </summary>
    /// <remarks>
    /// It uses System.Random function, therefore it should not be used
    /// for security-critical applications or for protecting sensitive data.
    /// </remarks>
    /// <param name="min">A boundary value, lowest possible.</param>
    /// <param name="max">A boundary value, highest possible.</param>
    /// <returns>New randomized decimal number.</returns>
    public virtual decimal GetRandomDecimal(int min = 0, int max = 9999) => RandomNext(min, max);

    /// <summary>
    /// Returns randomized stream of bytes.
    /// </summary>
    /// <remarks>
    /// It uses System.Random function, therefore it should not be used
    /// for security-critical applications or for protecting sensitive data.
    /// </remarks>
    /// <param name="sizeInKb">Expected size in kBytes.</param>
    /// <returns>New randomized stream of bytes.</returns>
    public virtual MemoryStream GetRandomStream(int sizeInKb = 12)
    {
        var byteBuffer = new byte[sizeInKb * 1024]; 
        _numberGenerator.GetBytes(byteBuffer);
        return new MemoryStream(byteBuffer);
    }
        
    /// <summary>
    /// Returns randomized e-mail address.
    /// </summary>
    /// <remarks>
    /// It uses System.Random function, therefore it should not be used
    /// for security-critical applications or for protecting sensitive data.
    /// </remarks>
    /// <param name="length">Expected length of the name, 12 characters by default.</param>
    /// <param name="domain">Domain, "gmail.com" by default.</param>
    /// <returns>New randomized e-mail address.</returns>
    public virtual string GetRandomEmail(int length = 12, string domain = "gmail.com") 
        => $"{GetRandomString(length, "", true)}@{domain}";

    /// <summary>
    /// Returns randomized string with given length and prefix (optional).
    /// </summary>
    /// <remarks>
    /// It uses System.Random function, therefore it should not be used
    /// for security-critical applications or for protecting sensitive data.
    /// </remarks>
    /// <param name="length">Expected length, 12 characters by default.</param>
    /// <param name="prefix">Optional prefix.</param>
    /// <param name="useAlphabetOnly">Generate string from alphabet letters only.</param>
    /// <returns>New randomized string.</returns>
    public virtual string GetRandomString(int length = 12, string prefix = "", bool useAlphabetOnly = false)
    {
        if (length == 0) 
            return string.Empty; 

        const string allChars = "!@#$%^&*()?\\/:'<>{}[]0123456789abcdefghijklmnopqrstuvwxyz";
        const string alphabetOnly = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        var randomString = new string(Enumerable.Repeat(useAlphabetOnly ? alphabetOnly : allChars, length)
            .Select(strings => strings[RandomNext(strings.Length)])
            .ToArray()); 

        if (!string.IsNullOrEmpty(prefix)) 
            return prefix.Trim() + randomString; 

        return randomString;
    }

    /// <summary>
    /// Returns randomized IP address, either IPv4 or IPv6.
    /// </summary>
    /// <remarks>
    /// It uses System.Random function, therefore it should not be used
    /// for security-critical applications or for protecting sensitive data.
    /// </remarks>
    /// <param name="shouldReturnIPv6">Allow to select IPv4 or IPv6.</param>
    /// <returns>New randomized IP address.</returns>
    public virtual IPAddress GetRandomIpAddress(bool shouldReturnIPv6 = false)
    {
        var bytes = shouldReturnIPv6 
            ? new byte[16] 
            : new byte[4];
            
        _numberGenerator.GetBytes(bytes);
        return new IPAddress(bytes);
    }

    private static int RandomNext(int min, int max) => RandomNumberGenerator.GetInt32(min, max);

    private static int RandomNext(int max) => RandomNumberGenerator.GetInt32(max);
}