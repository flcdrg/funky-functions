using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace net31
{
    public static class ServiceBusQueueTrigger
    {
        [FunctionName("ServiceBusQueueTrigger")]
        public static void Run([ServiceBusTrigger(
            topicName: "OfConversation", 
            subscriptionName: "net31",
            Connection = "ServiceBusConnection")] Message message, ILogger log)
        {
            var body = System.Text.Encoding.UTF8.GetString(message.Body);
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {message.MessageId}");
            log.LogWarning(body);
        }
    }
}
