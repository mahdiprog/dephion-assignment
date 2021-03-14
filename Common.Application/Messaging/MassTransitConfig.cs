using System;
using System.Runtime.Serialization;

namespace Common.Application.Messaging
{
    public class MassTransitConfig
    {
        public BusType BusType { get; set; }
        public RabbitMQ RabbitMQ { get; set; }
        public AmazonSqs AmazonSqs { get; set; }
        public ActiveMq ActiveMq { get; set; }
        public AzureServiceBus AzureServiceBus { get; set; }

    }

    public class RabbitMQ
    {
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class ActiveMq
    {
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class AmazonSqs
    {
        public string Region { get; set; }
        public string AccessKey  { get; set; }
        public string SecretKey { get; set; }
    }public class AzureServiceBus
    {
        public string Address { get; set; }
        public string KeyName   { get; set; }
        public string SharedAccessKey  { get; set; }
        public TimeSpan TokenTimeToLive   { get; set; }
        public string TokenScope  { get; set; }
    }

    public enum BusType
    {
        [EnumMember(Value = "RabbitMQ")]
        RabbitMQ,
        [EnumMember(Value = "AzureServiceBus")]
        AzureServiceBus,
        [EnumMember(Value = "ActiveMQ")]
        ActiveMQ,
        [EnumMember(Value = "AmazonSQS")]
        AmazonSQS
    }
}
