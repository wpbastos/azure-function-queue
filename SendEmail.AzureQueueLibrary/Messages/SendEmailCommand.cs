using SendEmail.AzureQueueLibrary.Infrastructure;

namespace SendEmail.AzureQueueLibrary.Messages
{
    public class SendEmailCommand : BaseQueueMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public SendEmailCommand() : base(RouteNames.EmailBox)
        {
        }
    }
}