using Microsoft.Extensions.DependencyInjection;
using SendEmail.AzureQueueLibrary.MessageSerializer;
using SendEmail.AzureQueueLibrary.QueueConnection;

namespace SendEmail.AzureQueueLibrary.Infrastructure
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddAzureQueueLibrary(this IServiceCollection services,
            string queueConnectionString)
        {
            services.AddSingleton(new QueueConfig(queueConnectionString));
            services.AddSingleton<ICloudQueueClientFactory, CloudQueueClientFactory>();
            services.AddSingleton<IMessageSerializer, JsonMessageSerializer>();
            services.AddTransient<IQueueCommunicator, QueueCommunicator>();
            return services;
        }
    }
}