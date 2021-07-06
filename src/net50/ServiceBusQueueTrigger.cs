using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace net50
{
    public static class ServiceBusQueueTrigger
    {
        [Function("ServiceBusQueueTrigger")]
        public static void Run([ServiceBusTrigger("cumber", Connection = "ServiceBusConnection")] string myQueueItem, FunctionContext context)
        {
            var logger = context.GetLogger("ServiceBusQueueTrigger");
            logger.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
