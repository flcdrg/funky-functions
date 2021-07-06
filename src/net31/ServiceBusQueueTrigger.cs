using System;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace net31
{
    public static class ServiceBusQueueTrigger
    {
        [FunctionName("ServiceBusQueueTrigger")]
        public static void Run([ServiceBusTrigger("cumber", Connection = "ServiceBusConnection")] Message message, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {message.MessageId}");
        }
    }
}
