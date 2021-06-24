using System;

namespace TokanPages.Backend.Core.Generators
{
    public abstract class EnumProvider : BaseClass
    {
        public static T GetRandomEnum<T>()
        {
            var LValues = Enum.GetValues(typeof(T)); 
            return (T)LValues.GetValue(Random.Next(LValues.Length));
        }
    }
}