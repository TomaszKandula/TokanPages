using System;
using System.IO;
using System.Linq;

namespace TestDataProvider
{
    
    public static class DataProvider
    {

		private static Random FRandom = new Random();

		public static int GetRandomInt(int AMin = 0, int AMax = 12)
		{
			return FRandom.Next(AMin, AMax + 1);
		}

		public static string GetRandomEmail(string ADomain)
		{
			return $"{GetRandomString(8)}@{ADomain}";
		}

		public static decimal GetRandomDecimal(int AMin = 0, int AMax = 9999)
		{
			return FRandom.Next(AMin, AMax);
		}

		public static string GetRandomString(int ALength = 12, string APrefix = "")
		{

			const string LChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

			var LString = new string(Enumerable.Repeat(LChars, ALength)
				.Select(AString => AString[FRandom.Next(AString.Length)])
				.ToArray());

			if (!string.IsNullOrEmpty(APrefix) || !string.IsNullOrWhiteSpace(APrefix)) 
				return APrefix + LString;

			return LString;

		}

		public static byte[] GetRandomByteArray(int ASizeInKb = 12)
		{
			var LByteBuffer = new byte[ASizeInKb * 1024];
			FRandom.NextBytes(LByteBuffer);
			return LByteBuffer;
		}

		public static MemoryStream GetRandomStreamData(int ASizeInKb = 12)
		{
			var LByteBuffer = GetRandomByteArray(ASizeInKb);
			return new MemoryStream(LByteBuffer);
		}

		public static DateTime GetRandomDate(DateTime? AMin = null, DateTime? AMax = null, int ADefaultYear = 2020)
		{

			if (!AMin.HasValue) AMin = new DateTime(ADefaultYear, 1, 1);
			if (!AMax.HasValue) AMax = new DateTime(ADefaultYear, 12, 31);

			var LDayRange = (AMax - AMin).Value.Days;
			return AMin.Value.AddDays(FRandom.Next(0, LDayRange));

		}

		public static T GetRandomEnum<T>()
		{

			var LRandom = new Random();
			var LValues = Enum.GetValues(typeof(T));

			return (T)LValues.GetValue(LRandom.Next(LValues.Length));

		}

	}

}
