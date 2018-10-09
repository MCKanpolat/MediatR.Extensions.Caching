namespace MediatR.Extensions.Caching
{
    public interface ISerializer<T> where T : new()
    {
        T Deserialize(byte[] source);
        byte[] Serialize(T source);
    }
}