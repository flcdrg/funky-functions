using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace net50
{
    public static class ServiceBusQueueTrigger
    {
        [Function("ServiceBusQueueTrigger")]
        public static void Run([ServiceBusTrigger(
            topicName: "OfConversation",
            subscriptionName: "net5",
            Connection = "ServiceBusConnection")] string myQueueItem, FunctionContext context) // ServiceBusReceivedMessage
        {
            var logger = context.GetLogger("ServiceBusQueueTrigger");
            logger.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
