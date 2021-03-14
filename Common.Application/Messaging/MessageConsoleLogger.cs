using System;
using System.Threading.Tasks;
using MassTransit.Audit;
using Newtonsoft.Json.Linq;

namespace Common.Application.Messaging
{
    public class MessageConsoleLogger : IMessageAuditStore
    {
        public Task StoreMessage<T>(T message, MessageAuditMetadata metadata) where T : class
        {
            Console.WriteLine("message sent from {0} to {1}:", metadata.SourceAddress,
                metadata.DestinationAddress);
            var parsed = JObject.FromObject(message);

            foreach (var pair in parsed) Console.WriteLine("\t{0}: {1}", pair.Key, pair.Value);
            return Task.CompletedTask;
        }
    }
}