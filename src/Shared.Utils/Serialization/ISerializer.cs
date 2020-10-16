namespace Shared.Utils.Serialization
{
    public interface ISerializer
    {
        T DeserializeObject<T>(string input);
    }
}
