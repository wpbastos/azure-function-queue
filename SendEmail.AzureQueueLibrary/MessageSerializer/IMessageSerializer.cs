namespace SendEmail.AzureQueueLibrary.MessageSerializer
{
    public interface IMessageSerializer
    {
        T Deserialize<T>(string message);
        string Serialize(object obj);
    }
}