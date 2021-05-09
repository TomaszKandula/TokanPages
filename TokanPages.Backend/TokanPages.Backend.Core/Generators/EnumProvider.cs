using System;

namespace TokanPages.Backend.Core.Generators
{
    public abstract class EnumProvider : BaseClass
    {
        public static T GetRandom<T>()
        {
            var LValues = Enum.GetValues(typeof(T)); 
            return (T)LValues.GetValue(Random.Next(LValues.Length));
        }
    }
}