using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Queue;
using SendEmail.AzureQueueLibrary.Messages;
using SendEmail.AzureQueueLibrary.MessageSerializer;

namespace SendEmail.AzureQueueLibrary.QueueConnection
{
    public interface IQueueCommunicator
    {
        T Read<T>(string message);
        Task SendAsync<T>(T obj) where T : BaseQueueMessage;
    }

    public class QueueCommunicator : IQueueCommunicator
    {
        private readonly ICloudQueueClientFactory _cloudQueueClientFactory;
        private readonly IMessageSerializer _messageSerializer;

        public QueueCommunicator(IMessageSerializer messageSerializer, ICloudQueueClientFactory cloudQueueClientFactory)
        {
            _messageSerializer = messageSerializer;
            _cloudQueueClientFactory = cloudQueueClientFactory;
        }

        public T Read<T>(string message)
        {
            return _messageSerializer.Deserialize<T>(message);
        }

        public async Task SendAsync<T>(T obj) where T : BaseQueueMessage
        {
            var queueReference = _cloudQueueClientFactory.GetClient().GetQueueReference(obj.Route);
            await queueReference.CreateIfNotExistsAsync();

            var serializedMessage = _messageSerializer.Serialize(obj);
            var queueMessage = new CloudQueueMessage(serializedMessage);

            await queueReference.AddMessageAsync(queueMessage);
        }
    }
}