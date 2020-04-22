using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendEmail.AzureFunctions.Email;
using SendEmail.AzureQueueLibrary.Infrastructure;

namespace SendEmail.AzureFunctions.Infrastructure
{
    public sealed class DIContainer
    {
        private static readonly IServiceProvider _instance = Build();
        public static IServiceProvider Instance => _instance;

        static DIContainer()
        {
        }

        private DIContainer()
        {
        }

        private static IServiceProvider Build()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            services.AddSingleton
            (
                new EmailConfig
                (
                    configuration["EmailHost"],
                    Convert.ToInt32(configuration["EmailPort"]),
                    configuration["EmailSender"],
                    configuration["EmailPassword"]
                )
            );
            services.AddSingleton<ISendEmailCommandHandler, SendEmailCommandHandler>();
            services.AddAzureQueueLibrary(configuration["AzureWebJobsStorage"]);
            return services.BuildServiceProvider();
        }
    }
}