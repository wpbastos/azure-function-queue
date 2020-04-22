using System;

namespace SendEmail.AzureFunctions.Email
{
    public class EmailConfig
    {
        public string Host { get; }
        public int Port { get; }
        public string Sender { get; }
        public string Password { get; }


        public EmailConfig(string host, int port, string sender, string password)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            Port = port;
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }
    }
}