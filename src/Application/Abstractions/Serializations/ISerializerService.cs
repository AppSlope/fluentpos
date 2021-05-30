namespace FluentPOS.Application.Abstractions.Serializations
{
    public interface ISerializerService
    {
        T DeserializeBytes<T>(byte[] bytes);

        byte[] Serialize<T>(T obj);
    }
}