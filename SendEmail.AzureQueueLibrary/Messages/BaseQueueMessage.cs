namespace SendEmail.AzureQueueLibrary.Messages
{
    public abstract class BaseQueueMessage
    {
        public string Route { get; }

        protected BaseQueueMessage(string route)
        {
            Route = route;
        }
    }
}