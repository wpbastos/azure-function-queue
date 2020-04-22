namespace SendEmail.AzureQueueLibrary.Infrastructure
{
    public class QueueConfig
    {
        public string QueueConnectionString { get; }

        public QueueConfig(string queueConnectionString)
        {
            QueueConnectionString = queueConnectionString;
        }
    }
}