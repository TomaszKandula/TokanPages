using System;

namespace Backend.TestData
{

    public static class GuidTest
    {

        public static bool Check(string AValue) 
        {
            return Guid.TryParse(AValue, out var _);
        }

    }

}
