using System;
using System.IO;
using System.Linq;

namespace TestDataProvider
{
    
    public static class DataProvider
    {

		private static Random FRandom = new Random();

		public static int GenerateRandomInt(int AMin = 0, int AMax = 12)
		{
			return FRandom.Next(AMin, AMax + 1);
		}

		public static string GenerateRandomString(int ALength = 12)
		{
			return GetRandomString(ALength);
		}

		public static string GenerateRandomEmail(string ADomain)
		{
			return $"{GetRandomString(8)}@{ADomain}";
		}

		public static decimal GenerateRandomDecimal(int AMin = 0, int AMax = 9999)
		{
			return FRandom.Next(AMin, AMax);
		}

		public static string GetRandomString(int ALength)
		{
			const string LChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			return new string(Enumerable.Repeat(LChars, ALength)
				.Select(AString => AString[FRandom.Next(AString.Length)])
                                .ToArray());
		}

		public static byte[] GenerateRandomByteArray(int ASizeInKb = 12)
		{
			var LByteBuffer = new byte[ASizeInKb * 1024];
			FRandom.NextBytes(LByteBuffer);
			return LByteBuffer;
		}

		public static MemoryStream GenerateRandomStreamData(int ASizeInKb = 12)
		{
			var LByteBuffer = GenerateRandomByteArray(ASizeInKb);
			return new MemoryStream(LByteBuffer);
		}

		public static DateTime GenerateRandomDate(DateTime? AMin = null, DateTime? AMax = null, int ADefaultYear = 2020)
		{

			if (!AMin.HasValue) AMin = new DateTime(ADefaultYear, 1, 1);
			if (!AMax.HasValue) AMax = new DateTime(ADefaultYear, 12, 31);

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
