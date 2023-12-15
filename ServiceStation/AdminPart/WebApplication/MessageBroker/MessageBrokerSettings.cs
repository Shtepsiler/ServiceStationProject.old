using MassTransit.Configuration;

namespace WebApplication.MessageBroker
{
    public class MessageBrokerSettings
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
