namespace TokanPages.Backend.Shared.Services.DataUtilityService
{
    using System;
    using System.IO;
    using System.Net;
    using System.Linq;

    public class DataUtilityService : DataUtilityObject, IDataUtilityService
    {
        private readonly Random FRandom;

        public DataUtilityService() => FRandom = new Random();

        /// <summary>
        /// Returns randomized Date within given range.
        /// </summary>
        /// <remarks>
        /// It uses System.Random function, therefore it should not be used
        /// for security-critical applications or for protecting sensitive data.
        /// </remarks>
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
        /// </remarks>
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
        /// </remarks>
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
        /// </remarks>
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
        /// </remarks>
        /// <param name="ASizeInKb">Expected size in kBytes.</param>
        /// <returns>New randomized stream of bytes.</returns>
        public override MemoryStream GetRandomStream(int ASizeInKb = 12)
        {
            var LByteBuffer = new byte[ASizeInKb * 1024]; 
            FRandom.NextBytes(LByteBuffer);
            return new MemoryStream(LByteBuffer);
        }
        
        /// <summary>
        /// Returns randomized e-mail address.
        /// </summary>
        /// <remarks>
        /// It uses System.Random function, therefore it should not be used
        /// for security-critical applications or for protecting sensitive data.
        /// </remarks>
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
        /// </remarks>
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
        /// Returns randomized IP address, either IPv4 or IPv6.
        /// </summary>
        /// <remarks>
        /// It uses System.Random function, therefore it should not be used
        /// for security-critical applications or for protecting sensitive data.
        /// </remarks>
        /// <param name="AShouldReturnIPv6">Allow to select IPv4 or IPv6.</param>
        /// <returns>New randomized IP address.</returns>
        public override IPAddress GetRandomIpAddress(bool AShouldReturnIPv6 = false)
        {
            var LBytes = AShouldReturnIPv6 
                ? new byte[16] 
                : new byte[4];
            
            FRandom.NextBytes(LBytes);
            return new IPAddress(LBytes);
        }
    }
}