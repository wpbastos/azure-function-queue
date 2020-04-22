using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SendEmail.AzureFunctions.Infrastructure;
using SendEmail.AzureQueueLibrary.Infrastructure;
using SendEmail.AzureQueueLibrary.Messages;
using SendEmail.AzureQueueLibrary.QueueConnection;

namespace SendEmail.AzureFunctions.Email
{
    public static class EmailQueueTrigger
    {
        [FunctionName("EmailQueueTrigger")]
        public static async Task Run(
            [QueueTrigger(RouteNames.EmailBox, Connection = "AzureWebJobsStorage")]
            string message,
            ILogger log)
        {
            try
            {
                var queueCommunicator = DIContainer.Instance.GetService<IQueueCommunicator>();
                var command = queueCommunicator.Read<SendEmailCommand>(message);
                var handler = DIContainer.Instance.GetService<ISendEmailCommandHandler>();

                await handler.Handle(command);
            }
            catch (Exception e)
            {
                log.LogError(e, $"Something went wrong with the EmailQueueTrigger - {message}");
                throw;
            }
        }
    }
}