using System.IO;

namespace TokanPages.Backend.Core.Generators
{
    public abstract class StreamProvider : BaseClass
    {
        public static MemoryStream GetRandomStream(int ASizeInKb = 12)
            => new MemoryStream(GetRandomByteArray(ASizeInKb));
        
        private static byte[] GetRandomByteArray(int ASizeInKb = 12)
        {
            var LByteBuffer = new byte[ASizeInKb * 1024]; 
            Random.NextBytes(LByteBuffer); 
            return LByteBuffer;
        }
    }
}