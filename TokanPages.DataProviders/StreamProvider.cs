using System.IO;

namespace TokanPages.DataProviders
{
    public abstract class StreamProvider : BaseClass
    {
        public static MemoryStream GetRandom(int ASizeInKb = 12)
            => new MemoryStream(GetRandomByteArray(ASizeInKb));
        
        private static byte[] GetRandomByteArray(int ASizeInKb = 12)
        {
            var LByteBuffer = new byte[ASizeInKb * 1024]; 
            FRandom.NextBytes(LByteBuffer); 
            return LByteBuffer;
        }
    }
}