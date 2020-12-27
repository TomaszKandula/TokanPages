using System;
using System.IO;
using System.Linq;

namespace TestDataProvider
{
    
    public static class DataProvider
    {

		private static Random FRandom = new Random();

		public static int GenerateRandomInt(int AMin = 0, int AMax = 10)
		{
			return FRandom.Next(AMin, AMax + 1);
		}

		public static string GenerateRandomString(int ALength = 10)
		{
			return GetRandomString(ALength);
		}

		public static string GenerateRandomEmail(string ADomain)
		{
			return $"{GetRandomString(8)}@{ADomain}";
		}

		public static decimal GenerateRandomDecimal(int AMin = 0, int AMax = 1000)
		{
			return FRandom.Next(AMin, AMax);
		}

		public static string GetRandomString(int ALength)
		{
			const string LChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			return new string(Enumerable.Repeat(LChars, ALength)
				.Select(s => s[FRandom.Next(s.Length)]).ToArray());
		}

		public static byte[] GenerateRandomByteArray(int ASizeInKb = 10)
		{
			var LByteBuffer = new byte[ASizeInKb * 1024];
			FRandom.NextBytes(LByteBuffer);
			return LByteBuffer;
		}

		public static MemoryStream GenerateRandomStreamData(int ASizeInKb = 10)
		{
			var LByteBuffer = GenerateRandomByteArray(ASizeInKb);
			return new MemoryStream(LByteBuffer);
		}

		public static DateTime GenerateRandomDate(DateTime? AMin = null, DateTime? AMax = null)
		{

			if (!AMin.HasValue) AMin = new DateTime(2020, 1, 1);
			if (!AMax.HasValue) AMax = new DateTime(2020, 12, 31);

			var LDayRange = (AMax - AMin).Value.Days;
			return AMin.Value.AddDays(FRandom.Next(0, LDayRange));

		}

		public static T GenerateRandomEnum<T>()
		{

			var LRandom = new Random();
			var LValues = Enum.GetValues(typeof(T));

			return (T)LValues.GetValue(LRandom.Next(LValues.Length));

		}

	}

}
