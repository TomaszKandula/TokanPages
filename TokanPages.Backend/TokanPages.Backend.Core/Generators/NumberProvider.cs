namespace TokanPages.Backend.Core.Generators
{
    public abstract class NumberProvider : BaseClass
    {
        public static int GetRandomInteger(int AMin = 0, int AMax = 12) 
            => Random.Next(AMin, AMax + 1);

        public static decimal GetRandomDecimal(int AMin = 0, int AMax = 9999) 
            => Random.Next(AMin, AMax);
    }
}