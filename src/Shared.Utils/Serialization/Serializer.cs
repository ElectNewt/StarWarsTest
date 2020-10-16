using System.Text.Json;

namespace Shared.Utils.Serialization
{
    public class Serializer : ISerializer
    {
        public T DeserializeObject<T>(string input)
        {
            return JsonSerializer.Deserialize<T>(input);
        }

    }
}
