using FluentPOS.Application.Abstractions.Serializations;
using Newtonsoft.Json;
using System.Text;

namespace FluentPOS.Infrastructure.Shared.Serializations
{
    public class SerializerService : ISerializerService
    {
        public T DeserializeBytes<T>(byte[] bytes) => JsonConvert.DeserializeObject<T>(Encoding.Default.GetString(bytes));

        public byte[] Serialize<T>(T obj) => Encoding.Default.GetBytes(JsonConvert.SerializeObject(obj));
    }
}
