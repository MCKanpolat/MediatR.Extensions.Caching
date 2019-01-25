namespace MediatR.Extensions.Caching
{
    public interface ISerializer
    {
        T Deserialize<T>(byte[] source);
        byte[] Serialize<T>(T source);
    }
}