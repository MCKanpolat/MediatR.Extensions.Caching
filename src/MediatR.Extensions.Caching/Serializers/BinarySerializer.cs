using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MediatR.Extensions.Caching.Serializers
{
    public class BinarySerializer<T> : ISerializer<T> where T : new()
    {
        private readonly BinaryFormatter binaryFormatter;
        public BinarySerializer()
        {
            binaryFormatter = new BinaryFormatter();
        }

        public T Deserialize(byte[] source)
        {
            using (var memoryStream = new MemoryStream(source))
            {
                return (T)binaryFormatter.Deserialize(memoryStream);
            }
        }

        public byte[] Serialize(T source)
        {
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, source);
                return memoryStream.ToArray();
            }
        }
    }
}